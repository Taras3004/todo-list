using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.CreateTag;

public class CreateTaskTagHandler(TodoListDbContext context) : IRequestHandler<CreateTaskTagCommand, TaskTagResponse>
{
    public async Task<TaskTagResponse> Handle(CreateTaskTagCommand request, CancellationToken cancellationToken)
    {
        TaskTag tag = new TaskTag()
        {
            Tag = request.Tag,
            Color = request.Color,
            UserId = "13213",
        };

        await context.TaskTags.AddAsync(tag, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagResponse()
        {
            Id = tag.Id,
            Color = tag.Color,
            Tag = tag.Tag,
        };
    }
}
