using MediatR;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Exceptions;

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
    }
}