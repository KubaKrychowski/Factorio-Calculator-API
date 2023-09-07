using Application.Repositories;
using Contracts.Items.Responses;
using Contracts.Recipes.Responses;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using MediatR;

namespace Application.Queries
{
    public class GetItemByIdQuery : IRequest<GetItemResponseDto>
    {
        public Guid ExternalId { get; set; }
        public GetItemByIdQuery(Guid externalId)
        {
            ExternalId = externalId;
        }
    }

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, GetItemResponseDto>
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IRecipeRepository _recipeRepository;

        public GetItemByIdQueryHandler(IItemsRepository itemsRepository, IRecipeRepository recipeRepository)
        {
            _itemsRepository = itemsRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<GetItemResponseDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemsRepository.GetByIdAsync(request.ExternalId, cancellationToken);

            if (item == null)
            {
                throw new NotFoundException(new[] { nameof(item), request.ExternalId.ToString() });
            }

            GetItemResponseDto response = new()
            {
                Name = item.Name,
                CategoryId = item.CategoryId,
                Id = item.ExternalId,
                Width = item.Width,
                Height = item.Height,
                Stars = item.Stars,
                IconUrl = item.IconUrl,
                PowerCost = item.PowerCost,
                Polution = item.Polution,
                Efficiency = new()
                {
                    EfficiencyPercentage = item.EfficiencyPercentage,
                    EfficiencyPerMinute = item.EfficiencyPerMinute
                },
                Recipes = await GetItemRecipesAsync(item.ExternalId, cancellationToken),
            };

            return response;
        }

        private async Task<List<GetItemRecipeResponseDto>> GetItemRecipesAsync(Guid itemExternalId, CancellationToken cancellationToken)
        {
            var itemRecipes = await _recipeRepository.GetByItemExternalIdAsync(itemExternalId, cancellationToken);

            var result = new List<GetItemRecipeResponseDto>();

            itemRecipes.ForEach(recipe =>
            {
                var recipeDto = new GetItemRecipeResponseDto()
                {
                    ExternalId = recipe.ExternalId,
                    Ingredients = recipe.Ingredients
                        .Select(ingredient => new IngredientModelDto()
                        {
                            Amount = ingredient.Amount,
                            ExternalId = ingredient.ExternalId,
                            ItemExternalId = ingredient.ItemExternalId
                        })
                        .ToList(),
                    ProductionTime = recipe.ProductionTime
                };

                result.Add(recipeDto);
            });

            return result;
        }
    }
}
