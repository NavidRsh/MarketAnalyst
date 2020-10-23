using MarketAnalyst.Core.Commands.Common;
using MarketAnalyst.Core.Commands.DataCollection;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.Handlers.DataCollection
{
    public class CalculateBuyingPowerHourlyHandler : IRequestHandler<CalculateBuyingPowerHourlyCommand, GenericBoolResponse>
    {
        IUnitOfWork _unitOfWork { get; set; }
        IHttpCallService _httpCallService { get; set; }
        public CalculateBuyingPowerHourlyHandler(IUnitOfWork unitOfWork, IHttpCallService httpCallService)
        {
            _unitOfWork = unitOfWork;
            _httpCallService = httpCallService;
        }
        public async Task<GenericBoolResponse> Handle(CalculateBuyingPowerHourlyCommand request, CancellationToken cancellationToken)
        {
            var stocksList = await _unitOfWork.StockService.GetStocksGeneralInfo(
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
                        var historyList = await _httpCallService.GetAlborzServiceAsync<string>(url, headers);
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
                                var dateString = generalInfo[generalInfo.Length - 2];
                                DateTime date = new DateTime(Helpers.Convertions.ToInt(dateString.Substring(0, 4)),
                                    Helpers.Convertions.ToInt(dateString.Substring(4, 2)), Helpers.Convertions.ToInt(dateString.Substring(6, 2)));
                                var timeString = generalInfo[generalInfo.Length - 1];
                                TimeSpan time = new TimeSpan(Helpers.Convertions.ToInt(timeString.Substring(0, 2)), Helpers.Convertions.ToInt(timeString.Substring(2, 2)), Helpers.Convertions.ToInt(timeString.Substring(4, 2)));

                                var lastBuyingPower = await _unitOfWork.BuyingPowerService.GetLastBuyingPowerOfDay(stock.Id, date, time);

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
                                        int buyPersonVolume, buyLegalVolume, sellPersonVolume, sellLegalVolume, buyPersonCount, buyLegalCount, sellPersonCount, sellLegalCount;
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
                                        await _unitOfWork.BuyingPowerService.Add(new Data.General.BuyingPower()
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
                                            AveragePersonBuy = buyPersonCount > 0 ? (buyPersonVolume / buyPersonCount) * finalPrice : 0,
                                            AverageLegalBuy = buyLegalCount > 0 ? (buyLegalVolume / buyLegalCount) * finalPrice : 0,
                                            RegisterDateTime = DateTime.Now                                            
                                        }); 
                                        await _unitOfWork.SaveAsync();
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

            return new GenericBoolResponse() { };
        }



    }
}
