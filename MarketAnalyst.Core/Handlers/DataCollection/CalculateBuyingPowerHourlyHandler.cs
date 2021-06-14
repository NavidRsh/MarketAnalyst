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
        private readonly Services.IBuyingPowerService _buyingPowerService; 
        public CalculateBuyingPowerHourlyHandler(Services.IBuyingPowerService buyingPowerService)
        {
            _buyingPowerService = buyingPowerService;
        }
        public async Task<GenericBoolResponse> Handle(CalculateBuyingPowerHourlyCommand request, CancellationToken cancellationToken)
        {
            await _buyingPowerService.CalcultePower();           

            return new GenericBoolResponse() { Success = true };
        }



    }
}
