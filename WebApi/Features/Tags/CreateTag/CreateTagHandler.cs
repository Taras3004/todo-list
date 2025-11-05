using MediatR;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.CreateTag;

public class CreateTagHandler(TodoListDbContext context) : IRequestHandler<CreateTagCommand, TaskTagDto>
{
    public async Task<TaskTagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        TaskTag tag = new TaskTag()
        {
            Tag = request.Tag,
        };

        await context.TaskTags.AddAsync(tag, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagDto()
        {
            Id = tag.Id,
            Tag = tag.Tag,
        };
    }
}
