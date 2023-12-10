using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Features.HealthCheck;
using System.Net;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Api.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HealthCheckController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("howAreYou")]
        [ProducesResponseType(typeof(Result<object>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> HealtCheck()
        {
            var result = await _mediator.Send(new HealthCheckQuery());
            return Ok(result);
        }
    }
}