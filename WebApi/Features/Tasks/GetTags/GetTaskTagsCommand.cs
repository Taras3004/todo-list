using MediatR;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTags;

public class GetTaskTagsCommand : IRequest<List<TaskTag>>
{
    public int TaskId { get; set; }
}
