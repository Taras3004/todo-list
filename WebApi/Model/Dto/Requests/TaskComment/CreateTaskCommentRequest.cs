public record CreateTaskCommentRequest
{
    public required string Content { get; init; }
    public int TodoTaskId { get; init; }
}
