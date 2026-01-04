namespace WebApi.Model.Dto.Requests.Lists;

public record UpdateListRequest
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }
}
