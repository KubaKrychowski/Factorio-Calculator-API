using Application.Repositories;
using Contracts.Items.Responses;
using Contracts.Recipes.Responses;
using Infrastructure.Models;
using MediatR;

namespace Application.Queries
{
    public class GetItemsQuery : IRequest<GetItemsQueryResponseDto>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, GetItemsQueryResponseDto>
    {
        private readonly IItemsRepository _itemsRepository;
        public GetItemsQueryHandler(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public async Task<GetItemsQueryResponseDto> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {

            var itemsData = await _itemsRepository.GetAllAsync(cancellationToken);

            GetItemsQueryResponseDto result = new()
            {
                Results = new List<GetItemResponseDto>()
            };

            if (!itemsData.Any())
            {
                return result;
            }

            itemsData.ForEach(itemData =>
            {
                List<GetItemRecipeResponseDto> recipes = new List<GetItemRecipeResponseDto>();

                itemData.Recipe.ToList().ForEach(recipeData =>
                {
                    GetItemRecipeResponseDto itemRecipe = new GetItemRecipeResponseDto()
                    {
                        ExternalId = recipeData.ExternalId,
                        ProductionTime = recipeData.ProductionTime,
                        Ingredients = recipeData.Ingredients.Select(ingredient => new IngredientModelDto()
                        {
                            ExternalId = ingredient.ExternalId,
                            Amount = ingredient.Amount,
                            ItemExternalId = ingredient.ItemExternalId,
                        }).ToList()

                    };

                    recipes.Add(itemRecipe);
                });

                GetItemResponseDto item = new()
                {
                    Id = itemData.Item.ExternalId,
                    Name = itemData.Item.Name,
                    CategoryId = itemData.Item.CategoryId,
                    Stars = itemData.Item.Stars,
                    Height = itemData.Item.Height,
                    Width = itemData.Item.Width,
                    PowerCost = itemData.Item.PowerCost,
                    Polution = itemData.Item.Polution,
                    IconUrl = itemData.Item.IconUrl,
                    Efficiency = new ItemEfficiencyResponseDto()
                    {
                        EfficiencyPercentage = itemData.Item.EfficiencyPercentage,
                        EfficiencyPerMinute = itemData.Item.EfficiencyPerMinute,
                    },
                    Recipes = recipes
                };

                result.Results.Add(item);
            });

            return result;
        }
    }
}

