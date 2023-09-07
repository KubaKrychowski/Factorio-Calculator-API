namespace Contracts.Items.Responses
{
    public class GetItemsQueryResponseDto
    {
        public required List<GetItemResponseDto> Results { get; set; }
    }
}
