using Application.Repositories;
using Contracts.Items.Requests;
using Contracts.Recipes.Requests;
using Domain.Consts;
using Domain.Entities;
using Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateItemCommand : IRequest<Unit>
    {
        public Guid ExternalId { get; }
        public string? Name { get; }
        public int? Stars { get; }
        public int? Height { get; }
        public int? Width { get; }
        public int? PowerCost { get; }
        public int? Polution { get; }
        public int? EfficiencyPercentage { get; }
        public int? EfficiencyPerMinute { get; }
        public ItemCategoryEnum? Category { get; }
        public Guid? RecipeId { get; }
        public IEnumerable<AddRecipeIngredientDto> Ingredients { get; }
        public int? ProductionTime { get; }

        public UpdateItemCommand(Guid externalId, UpdateItemRequestDto request, Guid? recipeId)
        {
            ExternalId = externalId;
            Name = request.Name;
            Stars = request.Stars;
            Height = request.Height;
            Width = request.Width;
            Category = request.Category;
            PowerCost = request.PowerCost;
            Polution = request.Polution;
            EfficiencyPercentage = request.EfficiencyPercentage;
            EfficiencyPerMinute = request.EfficiencyPerMinute;
            RecipeId = recipeId;
            Ingredients = request.Ingredients;
            ProductionTime = request.ProductionTime;
        }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IItemCategoryRepository _itemCategoryRepository;
        private readonly IRecipeRepository _recipeRepository;

        public UpdateItemCommandHandler(
            IItemCategoryRepository itemCategoryRepository,
            IItemsRepository itemsRepository,
            IRecipeRepository recipeRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
            _itemsRepository = itemsRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemsRepository.GetByIdAsync(request.ExternalId, cancellationToken)
                       ?? throw new NotFoundException(new[] { nameof(Item), request.ExternalId.ToString() });

            UpdateItemProperties(item, request);

            if (request.RecipeId != null)
            {
                await UpdateRecipeAsync(request.RecipeId.Value, request, cancellationToken);
            }

            await _itemsRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private static void UpdateItemProperties(Item item, UpdateItemCommand request)
        {
            if (request.Name != null)
            {
                item.Name = request.Name;
            }

            if (request.Stars != null)
            {
                item.Stars = request.Stars.Value;
            }

            if (request.Height != null)
            {
                item.Height = request.Height.Value;
            }

            if (request.Width != null)
            {
                item.Width = request.Width.Value;
            }

            if (request.Polution != null)
            {
                item.Polution = request.Polution.Value;
            }

            if (request.PowerCost != null)
            {
                item.PowerCost = request.PowerCost.Value;
            }

            if (request.EfficiencyPercentage != null)
            {
                item.EfficiencyPercentage = request.EfficiencyPercentage.Value;
            }

            if (request.EfficiencyPerMinute != null)
            {
                item.EfficiencyPerMinute = request.EfficiencyPerMinute.Value;
            }
        }

        private async Task UpdateRecipeAsync(Guid recipeId, UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeId, cancellationToken)
                         ?? throw new NotFoundException(new[] { nameof(recipeId), recipeId.ToString() });

            var recipeIngredients = request.Ingredients
                .Select(ingredientDto => new RecipeIngredient()
                {
                    ItemExternalId = ingredientDto.ItemExternalId,
                    Amount = ingredientDto.Amount,
                    RecipeExternalId = recipe.ExternalId
                })
                .ToList();

            if (request.ProductionTime != null)
            {
                recipe.SetProductionTime(request.ProductionTime.Value);
            }

            recipe.SetIngredients(recipeIngredients);
            await _recipeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
