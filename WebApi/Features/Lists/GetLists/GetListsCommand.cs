using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Lists.GetLists;

public class GetListsCommand : IRequest<List<TodoListDto>>;
