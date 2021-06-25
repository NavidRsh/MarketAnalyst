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
                var dayOfWeek = System.DateTime.Now.DayOfWeek;
                if (dayOfWeek == DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Thursday)
                {
                    //روزهای تعطیل تاخیر 2 ساعته
                    await Task.Delay(2 * 3600 * 1000, stoppingToken);
                }
                else
                {
                    if (System.DateTime.Now.TimeOfDay > new TimeSpan(9, 5, 0) &&
                        System.DateTime.Now.TimeOfDay < new TimeSpan(13, 0, 0))
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var powerService = scope.ServiceProvider.GetRequiredService(typeof(Core.Services.IBuyingPowerService));
                            await ((Core.Services.IBuyingPowerService)powerService).CalcultePower();

                            //تاخیر 10 دقیقه ای
                            await Task.Delay(600 * 1000, stoppingToken);
                        }
                    }
                    else//خارج از ساعات باز بودن بازار 
                    {
                        //محاسبه آمار روزانه 
                        if (Data.General.Setting.LastDailyPriceCalculation == null ||
                            Data.General.Setting.LastDailyPriceCalculation.Value.Date < DateTime.Now.Date)
                        {
                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var dailyPriceService = scope.ServiceProvider.GetRequiredService(typeof(Core.Services.IDailyPriceService));
                                await ((Core.Services.IDailyPriceService)dailyPriceService).CalculateDailyPrice(); 
                            }
                        }
                        //تاخیر نیم ساعته
                        await Task.Delay(1800 * 1000, stoppingToken);
                    }
                }
            }
        }
    }

}
