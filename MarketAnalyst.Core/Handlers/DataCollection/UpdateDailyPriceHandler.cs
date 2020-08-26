using HtmlAgilityPack;
using MarketAnalyst.Core.Commands.Common;
using MarketAnalyst.Core.Commands.DataCollection;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using MediatR;
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

            var result = _httpCallService.GetAlborzServiceAsync<string>("http://www.tsetmc.com/Loader.aspx?ParTree=111C1417", null);
            var finalResult = result.Result.Result;
            if (!string.IsNullOrEmpty(finalResult))
            {
                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(finalResult);
                HtmlNode node = pageDocument.DocumentNode.SelectSingleNode("//div");

                HtmlNode tableElement = pageDocument.GetElementbyId("tblToGrid");

                _unitOfWork.StockService.DeleteAll();
                foreach (HtmlNode rowElement in tableElement.ChildNodes)
                {

                    System.Collections.Generic.List<HtmlNode> items = rowElement.Descendants().Where(a => a.Name.ToLower() == "td").ToList();
                    if (items != null && items.Count() > 0)
                    {
                        string code = items[0].InnerText;
                        if (!ContainsUnicodeCharacter(code))
                        {
                            string groupName = items[2].InnerText;
                            var group = await _unitOfWork.StockGroupService.FindAsync(groupName);
                            int stockGroupId = 0;
                            if (group == null)
                            {
                                var grp = new Data.General.StockGroup()
                                {
                                    Code = "",
                                    Name = groupName
                                }; 
                                await _unitOfWork.StockGroupService.Add(grp);
                                await _unitOfWork.SaveAsync();
                                stockGroupId = grp.Id; 
                            }
                            else
                            {
                                stockGroupId = group.Id; 
                            }
                            await _unitOfWork.StockService.Add(new Data.General.Stock()
                            {
                                Code = code, 
                                StockGroupId = stockGroupId,
                                EnglishSign = items[4].InnerText,
                                EnglishName = items[5].InnerText,
                                PersianSign = items[6].InnerText,
                                PersianName = items[7].InnerText
                            });
                            await _unitOfWork.SaveAsync();
                        }
                    }
                }
            }


            return new GenericBoolResponse() { };
        }
        public bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.Any(c => c > MaxAnsiCode);
        }
    }
}
