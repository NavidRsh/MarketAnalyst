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
        [HttpPost("UpdateStocksInfo")]
        public async Task<IActionResult> UpdateStocksInfo()
        {
            return Ok(await _mediator.Send(new UpdateStocksInfoCommand()
            {
               
            }));
        }
    }
}