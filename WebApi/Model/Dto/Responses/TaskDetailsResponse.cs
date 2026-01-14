namespace WebApi.Model.Dto.Responses;

public record TaskDetailsResponse
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public DateTime Deadline { get; set; }

    public bool IsCompleted { get; set; }

    public string? Description { get; set; }

    public TaskTagResponse[]? TaskTags { get; set; }

    public required int TodoListId { get; set; }
}