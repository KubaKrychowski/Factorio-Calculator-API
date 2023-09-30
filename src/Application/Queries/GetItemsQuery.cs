using Application.Repositories;
using Contracts.Items;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var results = await _itemsRepository.GetAllAsync(cancellationToken);

            return new GetItemsQueryResponseDto()
            {
                Results = results.Select(x => new GetItemResponseDto { ExternalId = x.ExternalId }).ToList()
            };
        }
    }
}
