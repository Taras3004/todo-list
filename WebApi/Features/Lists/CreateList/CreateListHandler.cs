using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.CreateList;

public class CreateListHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<CreateListCommand, TodoListResponse>
{
    public async Task<TodoListResponse> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        TodoList todoList = new TodoList()
        {
            Name = request.Name,
            Description = request.Description,
            UserId = userId,
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
