using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MarketAnalyst.Api.Extensions;
using MarketAnalyst.Core.Data;
using MarketAnalyst.Core.DataService;
using MarketAnalyst.Core.Services.ExternalApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MarketAnalyst
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MarketContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IHttpCallService, HttpCallService>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Core.Services.IBuyingPowerService, Core.Services.BuyingPowerService>();
            services.AddScoped<Core.Services.IDailyPriceService, Core.Services.DailyPriceService>();
            services.AddHostedService<Core.Services.HostedServices.CalculatePowerHostedService>(); 

            services.AddSwaggerDocumentation();

            RegisterAllMediatRHandlers(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseMvc();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerDocumentation();
        }

        private void RegisterAllMediatRHandlers(IServiceCollection services)
        {
            var assemblyToScan = AppDomain.CurrentDomain.GetAssemblies();

            var types = assemblyToScan.SelectMany(s => s.GetTypes());

            foreach (var item in types.Where(c => c.FullName.StartsWith("MarketAnalyst.Core.Handlers") && c.FullName.EndsWith("Handler")))
            {
                services.AddMediatR(item.GetTypeInfo().Assembly);
            }

        }
    }
}
