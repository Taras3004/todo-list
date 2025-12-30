using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.CreateList;

public class CreateListHandler(TodoListDbContext context) : IRequestHandler<CreateListCommand, TodoListResponse>
{
    public async Task<TodoListResponse> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        TodoList todoList = new TodoList()
        {
            Name = request.Name,
            Description = request.Description,
            UserId = "1231321"
        };

        await context.Todos.AddAsync(todoList, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TodoListResponse()
        {
            Id = todoList.Id,
            Name = todoList.Name,
            Description = todoList.Description,
        };
    }
}
