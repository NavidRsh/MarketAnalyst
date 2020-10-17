using AngleSharp;
using AngleSharp.Scripting;
using HtmlAgilityPack;
using MarketAnalyst.Core.Commands.Common;
using MarketAnalyst.Core.Commands.DataCollection;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.Handlers.DataCollection
{
    public class UpdatePopularStocksInfoHandler : IRequestHandler<UpdatePopularStocksInfoCommand, GenericBoolResponse>
    {
        IUnitOfWork _unitOfWork { get; set; }
        IHttpCallService _httpCallService { get; set; }
        public UpdatePopularStocksInfoHandler(IUnitOfWork unitOfWork, IHttpCallService httpCallService)
        {
            _unitOfWork = unitOfWork;
            _httpCallService = httpCallService;
        }
        public async Task<GenericBoolResponse> Handle(UpdatePopularStocksInfoCommand request, CancellationToken cancellationToken)
        {

            string baseUrl = "http://www.tsetmc.com/";
            //ارسال درخواست برای گرفتن لیست پربیننده ترین نمادها

            //لیست پربیننده های فرابورس
            var faraBoorsList = _httpCallService.GetAlborzServiceAsync<string>(baseUrl + "Loader.aspx?Partree=151317&Type=MostVisited&Flow=2", null);
            var faraBoorsStocksList = faraBoorsList.Result.Result;
            if (!string.IsNullOrEmpty(faraBoorsStocksList))
            {
                //ثبت اطلاعات نمادهای موجود در لیست
                await SaveStockList(baseUrl, faraBoorsStocksList, Enums.MarketTypeEnum.FaraBoors);
            }

            //لیست پربیننده های بورس
            var boorsList = _httpCallService.GetAlborzServiceAsync<string>(baseUrl + "Loader.aspx?Partree=151317&Type=MostVisited&Flow=1", null);
            var boorsStocksList = boorsList.Result.Result;
            if (!string.IsNullOrEmpty(boorsStocksList))
            {
                //ثبت اطلاعات نمادهای موجود در لیست
                await SaveStockList(baseUrl, boorsStocksList, Enums.MarketTypeEnum.Boors);
            }

            return new GenericBoolResponse() { };
        }
        /// <summary>
        /// لیست نمادها رو یک به یک بازکرده و آدرس صفحه اطلاعاتی آن ها را بازیابی کرده و از طریق اون صفحه
        /// اطلاعات رو ثبت می کنه
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="finalStocksList"></param>
        /// <returns></returns>
        public async Task SaveStockList(string baseUrl, string finalStocksList, Enums.MarketTypeEnum marketType)
        {
            HtmlDocument stocksListDocument = new HtmlDocument();
            stocksListDocument.LoadHtml(finalStocksList);
            HtmlNode stockNameNode = stocksListDocument.DocumentNode.SelectSingleNode("//tbody");

            //_unitOfWork.StockService.DeleteAll(Enums.MarketTypeEnum.FaraBoors);
            //await _unitOfWork.SaveAsync();
            foreach (HtmlNode rowElement in stockNameNode.ChildNodes)
            {
                System.Collections.Generic.List<HtmlNode> items = rowElement.Descendants().Where(a => a.Name.ToLower() == "td").ToList();
                if (items != null && items.Count() > 0)
                {
                    string stockName = items[0].InnerText;
                    foreach (var atag in items[0].ChildNodes)
                    {
                        //آدرس مشخصات سهم 
                        string url = atag.Attributes["href"].Value;
                        if (!string.IsNullOrEmpty(url))
                        {
                            await ParsAndSaveStockInfo(baseUrl + url, marketType);
                        }
                    }

                }
            }
        }
        public async Task ParsAndSaveStockInfo(string infoUrl, Enums.MarketTypeEnum marketType)
        {
            try
            {
                var result = _httpCallService.GetAlborzServiceAsync<string>(infoUrl, null);
                var finalInfo = result.Result.Result;
                if (!string.IsNullOrEmpty(finalInfo))
                {
                    var configuration = Configuration.Default.WithJs();
                    var context = BrowsingContext.New(configuration);
                    var engine = context.GetService<JsScriptingService>();
                    var angleDocument = await context.OpenAsync(req => req.Content(finalInfo));

                    //نام گروه 
                    var lSecValue = engine.EvaluateScript(angleDocument, "LSecVal");
                    //کد گروه
                    var cgrValCot = engine.EvaluateScript(angleDocument, "CgrValCot");
                    //کدهای نماد
                    var instrumentID = engine.EvaluateScript(angleDocument, "InstrumentID");
                    var insCode = engine.EvaluateScript(angleDocument, "InsCode");
                    //عنوان کامل نماد
                    var title = engine.EvaluateScript(angleDocument, "Title");
                    //نام نماد
                    var lVal18AFC = engine.EvaluateScript(angleDocument, "LVal18AFC");
                    //حجم مبنا
                    var baseVol = engine.EvaluateScript(angleDocument, "BaseVol");
                    var estimatedEPS = engine.EvaluateScript(angleDocument, "EstimatedEPS");

                    //پی به ای گروه
                    var sectorPE = engine.EvaluateScript(angleDocument, "SectorPE");

                    string groupName = lSecValue.ToString();
                    var group = await _unitOfWork.StockGroupService.FindAsync(groupName);
                    int stockGroupId = 0;

                    //اضافه کردن یا بروزرسانی اطلاعات گروه
                    if (group == null)
                    {
                        var grp = new Data.General.StockGroup()
                        {
                            Code = cgrValCot.ToString(),
                            Name = groupName
                        };
                        await _unitOfWork.StockGroupService.Add(grp);
                        await _unitOfWork.SaveAsync();
                        stockGroupId = grp.Id;
                    }
                    else
                    {
                        group.Code = cgrValCot.ToString();
                        group.PE = Helpers.Convertions.Todouble(sectorPE);
                        _unitOfWork.StockGroupService.UpdateConnected(group);
                        await _unitOfWork.SaveAsync();

                        stockGroupId = group.Id;
                    }


                    var stock = await _unitOfWork.StockService.FindAsync(insCode.ToString());
                    if (stock == null)
                    {
                        await _unitOfWork.StockService.Add(new Data.General.Stock()
                        {
                            Code = instrumentID.ToString(),
                            UniqueCode = insCode.ToString(),
                            StockGroupId = stockGroupId,
                            BaseVolume = Helpers.Convertions.ToInt(baseVol),
                            //EnglishSign = items[4].InnerText,
                            //EnglishName = items[5].InnerText,
                            PersianSign = lVal18AFC.ToString(),
                            PersianName = title.ToString(),
                            MarketType = marketType,
                            EPS = Helpers.Convertions.ToInt(estimatedEPS),
                            InfoUrl = infoUrl
                        });
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        //بروزرسانی
                        //stock.BaseVolume = Helpers.Convertions.ToInt(baseVol); 

                    }

                    
                }
            }
            catch (Exception exc)
            {

            }
        }


    }
}
