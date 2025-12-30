
public record TaskCommentResponse
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime Created { get; set; }
    public int TodoTaskId { get; set; }
}