using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.UpdateTag;

public class UpdateTaskTagHandler(TodoListDbContext context) : IRequestHandler<UpdateTaskTagCommand, TaskTagResponse?>
{
    public async Task<TaskTagResponse?> Handle(UpdateTaskTagCommand request, CancellationToken cancellationToken)
    {
        var existingTag = await context.TaskTags
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (existingTag == null)
        {
            return null;
        }

        existingTag.Tag = request.Tag;

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagResponse()
        {
            Id = existingTag.Id,
            Tag = existingTag.Tag,
            Color = existingTag.Color,
        };
    }
}
