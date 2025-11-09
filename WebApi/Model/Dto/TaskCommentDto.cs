namespace WebApi.Model.Dto;

public record TaskCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public int TodoTaskId { get; set; }
}
