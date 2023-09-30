using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Items
{
    public class GetItemsQueryResponseDto
    {
        public List<GetItemResponseDto> Results { get; set; }
    }
}
