using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.CreateComment;

public class CreateTaskCommentHandler(TodoListDbContext context) : IRequestHandler<CreateTaskCommentCommand, TaskCommentResponse>
{
    public async Task<TaskCommentResponse> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        TaskComment taskComment = new TaskComment()
        {
            Content = request.Content,
            Created = DateTime.Now,
            TodoTaskId = request.TodoTaskId,
        };

        context.TaskComments.Add(taskComment);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskCommentResponse()
        {
            Id = taskComment.Id,
            Content = taskComment.Content,
            Created = taskComment.Created,
            TodoTaskId = taskComment.TodoTaskId,
        };
    }
}
