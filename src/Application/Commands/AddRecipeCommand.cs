using Application.Repositories;
using Contracts.Common;
using Contracts.Recipes.Requests;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Resources.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class AddRecipeCommand : IRequest<EntityCreatedResponseDto>
    {
        public Guid FinalItemId { get; set; }
        public IEnumerable<AddRecipeIngredientDto> Ingredients { get; set; }
        public int ProductionTime { get; set; }

        public AddRecipeCommand(AddRecipeRequestDto request)
        {
            FinalItemId = request.FinalItemId;
            Ingredients = request.Ingredients;
            ProductionTime = request.ProductionTime;
        }
    }

    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, EntityCreatedResponseDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IItemsRepository _itemsRepository;

        public AddRecipeCommandHandler(IRecipeRepository recipeRepository, IItemsRepository itemsRepository)
        {
            _recipeRepository = recipeRepository;
            _itemsRepository = itemsRepository;

        }

        public async Task<EntityCreatedResponseDto> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemsRepository.GetByIdAsync(request.FinalItemId, cancellationToken);

            if (item == null)
            {
                throw new MissingParamException(string.Format(Exceptions.MissingParam, nameof(request.FinalItemId)));
            }

            List<Guid> ingerdientDtoIds = request.Ingredients.Select(i => i.ItemExternalId).ToList();

            Recipe newRecipe = new(request.FinalItemId, request.ProductionTime, new List<RecipeIngredient>());

            foreach (var ingredientDto in request.Ingredients)
            {
                RecipeIngredient newIngredient = new()
                {
                    RecipeExternalId = newRecipe.ExternalId,
                    ItemExternalId = ingredientDto.ItemExternalId,
                    Amount = ingredientDto.Amount,
                };

                newRecipe.Ingredients.Add(newIngredient);
            }

            _recipeRepository.AddRecipeAsync(newRecipe);
            await _recipeRepository.SaveChangesAsync(cancellationToken);

            return new EntityCreatedResponseDto()
            {
                Id = item.ExternalId
            };
        }
    }
}
