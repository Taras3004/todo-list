using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.CreateTask;

public class CreateTaskCommand : IRequest<TaskDto>
{
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public string? Description { get; set; }
    public int TodoListId { get; set; }
}
