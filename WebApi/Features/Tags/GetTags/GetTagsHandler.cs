using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.GetTags;

public class GetTagsHandler(TodoListDbContext context) : IRequestHandler<GetTagsCommand, List<TaskTagDto>>
{
    public async Task<List<TaskTagDto>> Handle(GetTagsCommand request, CancellationToken cancellationToken)
    {
        var tags = await context.TaskTags.ToListAsync(cancellationToken);

        var tagsDto = new List<TaskTagDto>();

        foreach (var tag in tags)
        {
            tagsDto.Add(new TaskTagDto()
            {
                Id = tag.Id,
                Tag = tag.Tag
            });
        }

        return tagsDto;
    }
}
