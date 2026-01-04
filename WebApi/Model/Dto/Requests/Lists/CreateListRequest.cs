namespace WebApi.Model.Dto.Requests.Lists;

public record CreateListRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }
}
