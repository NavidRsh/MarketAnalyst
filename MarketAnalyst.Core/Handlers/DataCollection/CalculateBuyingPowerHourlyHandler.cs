using MarketAnalyst.Core.Commands.Common;
using MarketAnalyst.Core.Commands.DataCollection;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

                                    await _unitOfWork.BuyingPowerService.Add(new Data.General.BuyingPower()
                                    {
                                        StockId = stock.Id,
                                        Date = DateTime.Now.Date,
                                        TotalBuyPersonVolume = totalBuyPersonVolume,
                                        TotalBuyLegalVolume = totalBuyLegalVolume,
                                        TotalSellPersonVolume = totalSellPersonVolume,
                                        TotalSellLegalVolume = totalSellLegalVolume,
                                        TotalBuyPersonCount = totalBuyPersonCount,
                                        TotalBuyLegalCount = totalBuyLegalCount,
                                        TotalSellPersonCount = totalSellPersonCount,
                                        TotalSellLegalCount = totalSellLegalCount,
                                        TotalPersonBuyingPower = (totalBuyPersonCount != 0 && totalSellPersonCount != 0) ? Math.Round(((double)totalBuyPersonVolume / (double)totalBuyPersonCount) / ((double)totalSellPersonVolume / (double)totalSellPersonCount), 2) : 0,
                                        TotalLegalBuyingPower = (totalBuyLegalCount != 0 && totalSellLegalCount != 0 ) ? Math.Round(((double)totalBuyLegalVolume / (double)totalBuyLegalCount) / ((double)totalSellLegalVolume / (double)totalSellLegalCount), 2) : 0,
                                        RegisterDateTime = DateTime.Now
                                    }); ;
                                    await _unitOfWork.SaveAsync();
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
