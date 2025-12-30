
namespace WebApi.Model.Dto.Responses;

public record TaskCommentResponse
{
    public required int Id { get; set; }
    public required string Content { get; set; }
    public required DateTime Created { get; set; }
    public required int TodoTaskId { get; set; }
}
