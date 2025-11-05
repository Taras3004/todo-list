using MediatR;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.CreateComment;

public class CreateCommentHandler(TodoListDbContext context) : IRequestHandler<CreateCommentCommand, TaskCommentDto>
{
    public async Task<TaskCommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        TaskComment taskComment = new TaskComment()
        {
            Content = request.Content,
            Created = DateTime.Now,
            TodoTaskId = request.TodoTaskId,
        };

        context.TaskComments.Add(taskComment);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskCommentDto()
        {
            Id = taskComment.Id,
            Content = taskComment.Content,
            Created = taskComment.Created,
            TodoTaskId = taskComment.TodoTaskId,
        };
    }
}
