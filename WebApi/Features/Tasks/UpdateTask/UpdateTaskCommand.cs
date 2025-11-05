using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.UpdateTask;

public class UpdateTaskCommand : IRequest<TaskDto?>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public string? Description { get; set; }
}
