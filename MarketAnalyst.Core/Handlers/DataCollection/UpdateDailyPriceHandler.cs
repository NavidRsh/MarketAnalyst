using MarketAnalyst.Core.Commands.Common;
using MarketAnalyst.Core.Commands.DataCollection;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services;
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
        private readonly IDailyPriceService _dailyPriceService; 
        public UpdateDailyPriceHandler(IUnitOfWork unitOfWork, IHttpCallService httpCallService, 
            IDailyPriceService dailyPriceService)
        {
            _unitOfWork = unitOfWork;
            _httpCallService = httpCallService;
            _dailyPriceService = dailyPriceService; 
        }
        public async Task<GenericBoolResponse> Handle(UpdateDailyPriceCommand request, CancellationToken cancellationToken)
        {
            await _dailyPriceService.CalculateDailyPrice();             
            return new GenericBoolResponse() { };
        }



    }
}
