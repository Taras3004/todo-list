namespace WebApi.Model.Dto.Requests.Tasks;

public record AddTagToTaskRequest
{
    public required int TagId { get; init; }
}
