using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;
public class HomeController : Controller
{
    private readonly TodoApiClientContext clientContext;

    public HomeController(TodoApiClientContext clientContext)
    {
        this.clientContext = clientContext;
    }

    public IActionResult Index()
    {
        var todos = this.clientContext.GetEntitiesAsync<TodoList>().Result;

        var model = todos?.Select(todoList => new TodoListViewModel()
        {
            Instance = todoList
        }).ToList();

        return this.View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
