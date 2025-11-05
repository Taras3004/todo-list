using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.TagToTask.DeleteTagToTask;

public class DeleteTagToTaskHandler(TodoListDbContext context) : IRequestHandler<DeleteTagToTaskCommand, bool>
{
    public async Task<bool> Handle(DeleteTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.TagToTask.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            return false;
        }

        context.TagToTask.Remove(tag);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
