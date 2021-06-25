using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.Services
{
    public interface IBuyingPowerService
    {
        Task<bool> CalcultePower();
    }
    public class BuyingPowerService : IBuyingPowerService
    {
     
        private readonly IServiceScopeFactory _scopeFactory;
        public BuyingPowerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async Task<bool> CalcultePower()
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService(typeof(IUnitOfWork)) as IUnitOfWork;
                var httpClient = scope.ServiceProvider.GetRequiredService(typeof(IHttpCallService)) as IHttpCallService;

                var stocksList = await unitOfWork.StockService.GetStocksGeneralInfo(
               new List<Enums.SupervisionLevelEnum>()
               {
                    Enums.SupervisionLevelEnum.HighPriority
               });

                var priceListToAdd = new List<Data.General.StocksDailyPrice>();
                foreach (var stock in stocksList)
                {
                    priceListToAdd.Clear();
                    if (!string.IsNullOrEmpty(stock.UniqueCode))
                    {
                        try
                        {

                            string url = "http://www.tsetmc.com/tsev2/data/instinfofast.aspx?i=" + stock.UniqueCode + "&c=57%20";
                            //ارسال درخواست برای گرفتن سابقه نمادها
                            System.Threading.Thread.Sleep(100);
                            var headers = new Dictionary<string, string>();
                            headers.Add("Origin", "http://www.tsetmc.com");
                            headers.Add("Referer", "http://www.tsetmc.com/Loader.aspx?ParTree=151311&i=51617145873056483");
                            headers.Add("Host", "members.tsetmc.com");
                            headers.Add("Accept", "text/plain");
                            headers.Add("Connection", "keep-alive");
                            var historyList = await httpClient.GetAlborzServiceAsync<string>(url, headers);
                            var history = historyList.Result;
                            if (!string.IsNullOrEmpty(history))
                            {
                                //ثبت اطلاعات نمادهای موجود در لیست
                                var records = history.Split(';');
                                if (records.Length > 4)
                                {
                                    var generalInfo = records[0].Split(',');

                                    double lastPrice = Helpers.Convertions.Todouble(generalInfo[2]);//آخرین قیمت 
                                    double finalPrice = Helpers.Convertions.Todouble(generalInfo[3]);//قیمت پایانی
                                    double firstPrice = Helpers.Convertions.Todouble(generalInfo[4]);//قیمت بازگشایی روز
                                    double previousDayPrice = Helpers.Convertions.Todouble(generalInfo[5]);//قیمت روز قبل
                                    double highestPrice = Helpers.Convertions.Todouble(generalInfo[6]);//بیشترین قیمت
                                    double lowestPrice = Helpers.Convertions.Todouble(generalInfo[7]);//کمترین قیمت
                                    var dateString = generalInfo[generalInfo.Length - 2];
                                    DateTime date = new DateTime(Helpers.Convertions.ToInt(dateString.Substring(0, 4)),
                                        Helpers.Convertions.ToInt(dateString.Substring(4, 2)), Helpers.Convertions.ToInt(dateString.Substring(6, 2)));
                                    var timeString = generalInfo[generalInfo.Length - 1];
                                    timeString = timeString.Length == 5 ? timeString.PadLeft(6, '0') : timeString;
                                    TimeSpan time = new TimeSpan(Helpers.Convertions.ToInt(timeString.Substring(0, 2)), Helpers.Convertions.ToInt(timeString.Substring(2, 2)), Helpers.Convertions.ToInt(timeString.Substring(4, 2)));

                                    var lastBuyingPower = await unitOfWork.BuyingPowerService.GetLastBuyingPowerOfDay(stock.Id, date, time);

                                    //در صورتی که قدرت خرید در آن زمان ثبت نشده باشد آن را ثبت میکنیم
                                    if (lastBuyingPower == null || lastBuyingPower.EndTime != time)
                                    {
                                        var buyingPowerRecord = records[4];
                                        var powerInfo = buyingPowerRecord.Split(',');
                                        if (powerInfo.Length > 8)
                                        {
                                            int totalBuyPersonVolume = Helpers.Convertions.ToInt(powerInfo[0]);
                                            int totalBuyLegalVolume = Helpers.Convertions.ToInt(powerInfo[1]);
                                            int totalSellPersonVolume = Helpers.Convertions.ToInt(powerInfo[3]);
                                            int totalSellLegalVolume = Helpers.Convertions.ToInt(powerInfo[4]);
                                            int totalBuyPersonCount = Helpers.Convertions.ToInt(powerInfo[5]);
                                            int totalBuyLegalCount = Helpers.Convertions.ToInt(powerInfo[6]);
                                            int totalSellPersonCount = Helpers.Convertions.ToInt(powerInfo[8]);
                                            int totalSellLegalCount = Helpers.Convertions.ToInt(powerInfo[9]);


                                            long buyPersonVolume, buyLegalVolume, sellPersonVolume, sellLegalVolume;
                                            int buyPersonCount, buyLegalCount, sellPersonCount, sellLegalCount;
                                            buyPersonVolume = buyLegalVolume = sellPersonVolume = sellLegalVolume = buyPersonCount = buyLegalCount = sellPersonCount = sellLegalCount = 0;
                                            if (lastBuyingPower != null)
                                            {
                                                buyPersonVolume = totalBuyPersonVolume - lastBuyingPower.TotalBuyPersonVolume;
                                                buyLegalVolume = totalBuyLegalVolume - lastBuyingPower.TotalBuyLegalVolume;
                                                sellPersonVolume = totalSellPersonVolume - lastBuyingPower.TotalSellPersonVolume;
                                                sellLegalVolume = totalSellLegalVolume - lastBuyingPower.TotalSellLegalVolume;
                                                buyPersonCount = totalBuyPersonCount - lastBuyingPower.TotalBuyPersonCount;
                                                buyLegalCount = totalBuyLegalCount - lastBuyingPower.TotalBuyLegalCount;
                                                sellPersonCount = totalSellPersonCount - lastBuyingPower.TotalSellPersonCount;
                                                sellLegalCount = totalSellLegalCount - lastBuyingPower.TotalSellLegalCount;
                                            }
                                            await unitOfWork.BuyingPowerService.Add(new Data.General.BuyingPower()
                                            {
                                                StockId = stock.Id,
                                                Date = date,
                                                StartTime = lastBuyingPower != null ? lastBuyingPower.EndTime : new TimeSpan(9, 0, 0),
                                                EndTime = time,
                                                TotalBuyPersonVolume = totalBuyPersonVolume,
                                                TotalBuyLegalVolume = totalBuyLegalVolume,
                                                TotalSellPersonVolume = totalSellPersonVolume,
                                                TotalSellLegalVolume = totalSellLegalVolume,
                                                TotalBuyPersonCount = totalBuyPersonCount,
                                                TotalBuyLegalCount = totalBuyLegalCount,
                                                TotalSellPersonCount = totalSellPersonCount,
                                                TotalSellLegalCount = totalSellLegalCount,
                                                TotalPersonBuyingPower = (totalBuyPersonCount != 0 && totalSellPersonCount != 0) ? Math.Round(((double)totalBuyPersonVolume / (double)totalBuyPersonCount) / ((double)totalSellPersonVolume / (double)totalSellPersonCount), 2) : 0,
                                                TotalLegalBuyingPower = (totalBuyLegalCount != 0 && totalSellLegalCount != 0) ? Math.Round(((double)totalBuyLegalVolume / (double)totalBuyLegalCount) / ((double)totalSellLegalVolume / (double)totalSellLegalCount), 2) : 0,
                                                TotalAveragePersonBuy = totalBuyPersonCount > 0 ? (totalBuyPersonVolume / totalBuyPersonCount) * finalPrice : 0,
                                                TotalAverageLegalBuy = totalBuyLegalCount > 0 ? (totalBuyLegalVolume / totalBuyLegalCount) * finalPrice : 0,
                                                BuyPersonVolume = buyPersonVolume,
                                                BuyLegalVolume = buyLegalVolume,
                                                SellPersonVolume = sellPersonVolume,
                                                SellLegalVolume = sellLegalVolume,
                                                BuyPersonCount = buyPersonCount,
                                                BuyLegalCount = buyLegalCount,
                                                SellPersonCount = sellPersonCount,
                                                SellLegalCount = sellLegalCount,
                                                PersonBuyingPower = (buyPersonCount != 0 && sellPersonCount != 0) ? Math.Round(((double)buyPersonVolume / (double)buyPersonCount) / ((double)sellPersonVolume / (double)sellPersonCount), 2) : 0,
                                                FinalPrice = finalPrice,
                                                LastPrice = lastPrice,
                                                LowestPrice = lowestPrice,
                                                HighestPrice = highestPrice,
                                                FirstPrice = firstPrice,
                                                PreviousDayPrice = previousDayPrice,
                                                LastPriceChangePercent = (lastPrice - previousDayPrice) * 100 / previousDayPrice,
                                                FinalPriceChangePercent = (finalPrice - previousDayPrice) * 100 / previousDayPrice,
                                                AveragePersonBuy = buyPersonCount > 0 ? ((double)buyPersonVolume / buyPersonCount) * finalPrice : 0,
                                                AverageLegalBuy = buyLegalCount > 0 ? ((double)buyLegalVolume / buyLegalCount) * finalPrice : 0,
                                                RegisterDateTime = DateTime.Now
                                            });
                                            await unitOfWork.SaveAsync();
                                        }
                                    }
                                }
                                else
                                { }
                            }
                            else
                            { }
                        }
                        catch (Exception exc)
                        {

                        }
                    }
                }
                return true;
            }
        }
    }
}
