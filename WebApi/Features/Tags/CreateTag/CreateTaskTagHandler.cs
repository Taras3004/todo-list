using MediatR;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.CreateTag;

public class CreateTaskTagHandler(TodoListDbContext context) : IRequestHandler<CreateTaskTagCommand, TaskTagResponse>
{
    public async Task<TaskTagResponse> Handle(CreateTaskTagCommand request, CancellationToken cancellationToken)
    {
        TaskTag tag = new TaskTag()
        {
            Tag = request.Tag,
        };

        await context.TaskTags.AddAsync(tag, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagResponse()
        {
            Id = tag.Id,
            Tag = tag.Tag,
        };
    }
}
