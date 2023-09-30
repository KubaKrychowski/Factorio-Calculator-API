using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries;

namespace WebApi.Controllers.Items
{
    /// <summary>
    /// Items management
    /// </summary>
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Items controller
        /// </summary>
        /// <param name="mediator"></param>
        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetItemsQuery(), cancellationToken);
            return Ok(response);
        }
    }
}