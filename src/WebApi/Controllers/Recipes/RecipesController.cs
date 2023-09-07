using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries;
using Contracts.Items.Requests;
using Application.Commands;
using Infrastructure.Exceptions;
using Contracts.Recipes.Requests;

namespace WebApi.Controllers.Recipies
{
    /// <summary>
    /// Items management
    /// </summary>
    [ApiController]
    [Route("api/recipies")]
    public class RecipiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Recipies controller
        /// </summary>
        /// <param name="mediator"></param>
        public RecipiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new recipe
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddRecipeRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRecipeCommand(request), cancellationToken);
            return CreatedAtAction(nameof(AddRecipe), result);
        }
    }
}