using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Tasks.GetTasks;

public class GetTasksCommand() : IRequest<List<TaskResponse>>
{
    public int TodoListId { get; set; }
}
