using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Lists.GetLists;

public class GetListsCommand : IRequest<List<TodoListResponse>>;
