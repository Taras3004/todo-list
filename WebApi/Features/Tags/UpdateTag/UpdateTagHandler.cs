using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.UpdateTag;

public class UpdateTagHandler(TodoListDbContext context) : IRequestHandler<UpdateTagCommand, TaskTagDto?>
{
    public async Task<TaskTagDto?> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var existingTag = await context.TaskTags
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (existingTag == null)
        {
            return null;
        }

        existingTag.Tag = request.Tag; 

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagDto()
        {
            Id = existingTag.Id,
            Tag = existingTag.Tag,
        };
    }
}
