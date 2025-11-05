using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.UpdateTask;

public class UpdateTaskCommand(int id, string name, DateTime deadline, bool isCompleted, string? description)
    : IRequest<TaskDto?>
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public DateTime Deadline { get; } = deadline;
    public bool IsCompleted { get; } = isCompleted;
    public string? Description { get; } = description;
}
