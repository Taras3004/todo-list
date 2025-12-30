public record UpdateTaskTagRequest
{
    public int Id { get; init; }
    public required string Tag { get; init; }
}
