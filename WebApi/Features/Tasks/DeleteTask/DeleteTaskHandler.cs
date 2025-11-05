using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.DTO;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.DeleteTask;

public class DeleteTaskHandler(TodoListDbContext context) : IRequestHandler<DeleteTaskByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return false;
        }

        context.Tasks.Remove(task);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
