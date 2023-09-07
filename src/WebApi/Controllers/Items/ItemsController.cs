using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries;
using Contracts.Items.Requests;
using Application.Commands;
using Infrastructure.Exceptions;

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

        /// <summary>
        /// Get all items
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetItems(CancellationToken cancellationToken)
        {
            var items = await _mediator.Send(new GetItemsQuery(), cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Get item by externalId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetItemByExternalId(Guid id, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id), cancellationToken);
            return Ok(item);
        }

        /// <summary>
        /// Add new item 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddItemCommand(request), cancellationToken);

            if (request.Recipe != null)
            {
                await _mediator.Send(new AddRecipeCommand(request.Recipe), cancellationToken);
            }

            return CreatedAtAction(nameof(AddItem), result);
        }

        /// <summary>
        /// Update item properties
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateItem([FromQuery] Guid recipeId, Guid id, UpdateItemRequestDto request, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new MissingParamException(nameof(id));
            }

            var result = await _mediator.Send(new UpdateItemCommand(id, request, recipeId), cancellationToken);
            return Ok(result);
        }
    }
}