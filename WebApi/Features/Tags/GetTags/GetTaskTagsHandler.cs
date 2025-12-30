using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.GetTags;

public class GetTagsHandler(TodoListDbContext context) : IRequestHandler<GetTaskTagsCommand, List<TaskTagResponse>>
{
    public async Task<List<TaskTagResponse>> Handle(GetTaskTagsCommand request, CancellationToken cancellationToken)
    {
        var tags = await context.TaskTags.ToListAsync(cancellationToken);

        var tagsDto = new List<TaskTagResponse>();

        foreach (var tag in tags)
        {
            tagsDto.Add(new TaskTagResponse()
            {
                Id = tag.Id,
                Tag = tag.Tag
            });
        }

        return tagsDto;
    }
}
