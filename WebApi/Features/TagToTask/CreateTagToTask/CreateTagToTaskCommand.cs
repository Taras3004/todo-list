using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.TagToTask.CreateTagToTask;

public class CreateTagToTaskCommand(string name, string description) : IRequest<TagToTaskDto?>
{
    public int TodoTaskId { get; set; }
    public int TaskTagId { get; set; }
}
