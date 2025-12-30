using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.GetTagById;

public class GetTaskTagByIdHandler(TodoListDbContext context) : IRequestHandler<GetTaskTagByIdCommand, TaskTagResponse?>
{
    public async Task<TaskTagResponse?> Handle(GetTaskTagByIdCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            return null;
        }

        return new TaskTagResponse()
        {
            Id = tag.Id,
            Tag = tag.Tag,
        };
    }
}
