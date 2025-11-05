using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.DeleteList;

public class DeleteListHandler(TodoListDbContext context) : IRequestHandler<DeleteListCommand, bool>
{
    public async Task<bool> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        var comment = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            return false;
        }

        context.Todos.Remove(comment);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
