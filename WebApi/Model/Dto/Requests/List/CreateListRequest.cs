namespace WebApi.Model.Dto.Requests.List;

public record CreateListRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }
}
