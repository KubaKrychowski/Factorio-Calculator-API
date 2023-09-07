using Application.Repositories;
using Contracts.Common;
using Contracts.Items.Requests;
using Domain.Consts;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Application.Commands
{
    public class AddItemCommand : IRequest<EntityCreatedResponseDto>
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int PowerCost { get; set; }
        public ItemCategoryEnum Category { get; set; }

        public AddItemCommand(AddItemRequestDto request)
        {
            Name = request.Name;
            Stars = request.Stars;
            Height = request.Height;
            Width = request.Width;
            Category = request.Category;
            PowerCost = request.PowerCost;
        }
    }

    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, EntityCreatedResponseDto>
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IItemCategoryRepository _itemCategoryRepository;
        private readonly IConfiguration _configuration;

        public AddItemCommandHandler(IItemsRepository itemsRepository, IItemCategoryRepository itemCategoryRepository, IConfiguration configuration)
        {
            _itemsRepository = itemsRepository;
            _itemCategoryRepository = itemCategoryRepository;
            _configuration = configuration;
        }

        public async Task<EntityCreatedResponseDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var category = await _itemCategoryRepository.GetByIdAsync(request.Category, cancellationToken);

            Item item = new()
            {
                Name = request.Name,
                Stars = request.Stars,
                Height = request.Height,
                Width = request.Width,
                PowerCost = request.PowerCost,
                CategoryId = category.Id,
                IconUrl = GetIconUrl(request.Name, request.Category)
            };

            await _itemsRepository.AddItemAsync(item, cancellationToken);

            EntityCreatedResponseDto result = new()
            {
                Id = item.ExternalId
            };

            return result;
        }

        private string GetIconUrl(string name, ItemCategoryEnum categoryId)
        {
            StringBuilder sb = new StringBuilder($"{_configuration.GetConnectionString("BLOB")}/icons");

            switch (categoryId)
            {
                case ItemCategoryEnum.LOGISTICS:
                    {
                        sb.Append("/structures");
                        break;
                    }

                case ItemCategoryEnum.PRODUCTS:
                    {
                        sb.Append("/items");
                        break;
                    }

                case ItemCategoryEnum.COMBAT:
                    {
                        sb.Append("/weapons");
                        break;
                    }
            }

            return sb.Append($"/{name}.png").ToString();
        }
    }
}
