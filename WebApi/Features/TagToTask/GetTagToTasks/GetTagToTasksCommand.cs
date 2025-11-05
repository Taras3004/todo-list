using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.TagToTask.GetTagToTasks;

public class GetTagToTasksCommand : IRequest<List<TagToTaskDto>>;
