using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tasks.GetTasks;

public class GetTasksCommand() : IRequest<List<TaskDto>>
{
    public int TodoListId { get; set; }
}
