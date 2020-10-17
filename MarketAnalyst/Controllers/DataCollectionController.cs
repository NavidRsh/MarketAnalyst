using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAnalyst.Core.Commands.DataCollection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalyst.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCollectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DataCollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// نام و اطلاعات نمادهای بورس پایه رو فراخوانی میکند
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateStocksInfo")]
        public async Task<IActionResult> UpdateStocksInfo()
        {
            return Ok(await _mediator.Send(new UpdateStocksInfoCommand()
            {
               
            }));
        }
        /// <summary>
        /// از طریق پربیننده های فرابورس اطلاعات رو بازیابی می کند
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdatePopularStocksInfo")]
        public async Task<IActionResult> UpdatePopularStocksInfo()
        {
            return Ok(await _mediator.Send(new UpdatePopularStocksInfoCommand()
            {

            }));
        }


        /// <summary>
        /// از طریق صفحه سابقه نماد اطلاعات روزانه نماد را میگیرد
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateDailyPrice")]
        public async Task<IActionResult> UpdateDailyPrice()
        {
            return Ok(await _mediator.Send(new UpdateDailyPriceCommand()
            {

            }));
        }

        [HttpPost("CalculateBuyingPowerHourly")]
        public async Task<IActionResult> CalculateBuyingPowerHourly()
        {
            return Ok(await _mediator.Send(new CalculateBuyingPowerHourlyCommand()
            {

            }));
        }
    }
}