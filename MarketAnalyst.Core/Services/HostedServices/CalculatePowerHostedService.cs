using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.Services.HostedServices
{
    public class CalculatePowerHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CalculatePowerHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var powerService = scope.ServiceProvider.GetRequiredService(typeof(Core.Services.IBuyingPowerService));
                    await ((Core.Services.IBuyingPowerService)powerService).CalcultePower();

                    await Task.Delay(60000, stoppingToken);
                }
            }

        }
    }
}
