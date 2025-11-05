using MediatR;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.CreateList;

public class CreateListHandler(TodoListDbContext context) : IRequestHandler<CreateListCommand, TodoListDto>
{
    public async Task<TodoListDto> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        TodoList todoList = new TodoList()
        {
            Name = request.Name,
            Description = request.Description,
        };

        await context.Todos.AddAsync(todoList, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TodoListDto()
        {
            Id = todoList.Id,
            Name = todoList.Name,
            Description = todoList.Description,
        };
    }
}
