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
    public class UpdateDailyPriceHandler : IRequestHandler<UpdateDailyPriceCommand, GenericBoolResponse>
    {
        IUnitOfWork _unitOfWork { get; set; }
        IHttpCallService _httpCallService { get; set; }
        public UpdateDailyPriceHandler(IUnitOfWork unitOfWork, IHttpCallService httpCallService)
        {
            _unitOfWork = unitOfWork;
            _httpCallService = httpCallService;
        }
        public async Task<GenericBoolResponse> Handle(UpdateDailyPriceCommand request, CancellationToken cancellationToken)
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
                        //string url = "http://members.tsetmc.com/tsev2/data/InstTradeHistory.aspx?i=51617145873056483&Top=999999&A=0";
                        //string url = "http://members.tsetmc.com/tsev2/data/InstTradeHistory.aspx?i=" + stock.UniqueCode + "&Top=999999&A=0";
                        string url = "http://members.tsetmc.com/tsev2/data/InstTradeHistory.aspx?i=" + stock.UniqueCode + "&Top=30&A=0";
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
                            var previouslySavedDates = await _unitOfWork.StocksDailyPriceService.GetDates(stock.Id);


                            foreach (var record in records)
                            {
                                string[] itemsData = record.Split('@');
                                if (itemsData[0].Length == 8)
                                {
                                    DateTime date = new System.DateTime(Helpers.Convertions.ToInt(itemsData[0].Substring(0, 4)),
                                        Helpers.Convertions.ToInt(itemsData[0].Substring(4, 2)),
                                        Helpers.Convertions.ToInt(itemsData[0].Substring(6, 2)));


                                    //if (!await _unitOfWork.StocksDailyPriceService.Any(stock.Id, date))
                                    if (!previouslySavedDates.Any(a => a.Date == date.Date))
                                    {
                                        double highest = Helpers.Convertions.Todouble(itemsData[1]);
                                        double lowest = Helpers.Convertions.Todouble(itemsData[2]);
                                        double final = Helpers.Convertions.Todouble(itemsData[3]);
                                        double last = Helpers.Convertions.Todouble(itemsData[4]);
                                        double initial = Helpers.Convertions.Todouble(itemsData[5]);
                                        double previousDay = Helpers.Convertions.Todouble(itemsData[6]);

                                        double finalChange = final - previousDay;
                                        double finalChangePercent = previousDay != 0 ? finalChange * 100 / previousDay : 0;

                                        double lastChange = last - previousDay;
                                        double lastChangePercent = previousDay != 0 ? lastChange * 100 / previousDay : 0;

                                        double value = Helpers.Convertions.Todouble(itemsData[7]);
                                        long volume = Helpers.Convertions.ToLong(itemsData[8]);
                                        int count = Helpers.Convertions.ToInt(itemsData[9]);

                                        var newItem = new Data.General.StocksDailyPrice()
                                        {
                                            InitialPrice = Math.Round(initial, 0),
                                            FinalPrice = Math.Round(final, 0),
                                            LastPrice = Math.Round(last, 0),
                                            LowestPrice = Math.Round(lowest, 0),
                                            HighestPrice = Math.Round(highest, 0),
                                            Date = date,
                                            StockId = stock.Id,
                                            LastPriceChange = Math.Round(lastChange, 0),
                                            LastPriceChangePercent = Math.Round(lastChangePercent, 4),
                                            FinalPriceChange = Math.Round(finalChange, 0),
                                            FinalPriceChangePercent = Math.Round(finalChangePercent, 4),
                                            DealsCount = count,
                                            DealsValue = Math.Round((decimal)value, 0),
                                            DealsVolume = volume,
                                            PreviousDayPrice = Math.Round(previousDay, 0),
                                        };

                                        priceListToAdd.Add(newItem);
                                        //await _unitOfWork.StocksDailyPriceService.AddRange(new List<Data.General.StocksDailyPrice>() { newItem });
                                        //await _unitOfWork.SaveAsync();
                                    }

                                }
                            }
                            if (priceListToAdd.Count > 0)
                            {
                                await _unitOfWork.StocksDailyPriceService.AddRange(priceListToAdd);
                                await _unitOfWork.SaveAsync();
                            }
                        }
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
