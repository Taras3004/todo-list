using System.Net;
using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.GetTasks;

public class GetTasksCommand() : IRequest<List<TaskDto>>;
