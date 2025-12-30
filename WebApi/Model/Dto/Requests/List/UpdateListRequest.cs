public record UpdateListRequest
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }
}