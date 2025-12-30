using MediatR;

namespace WebApi.Features.Tasks.CreateTask;

public class CreateTaskCommand(string name, DateTime deadline, string? description, int todoListId) : IRequest<TaskResponse>
{
    public string Name { get; } = name;
    public DateTime Deadline { get; } = deadline;
    public string? Description { get; } = description;
    public int TodoListId { get; } = todoListId;
}
