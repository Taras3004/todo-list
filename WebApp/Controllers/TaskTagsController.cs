using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;

public class TaskTagsController : Controller
{
    private readonly TodoApiClientContext clientContext;

    public TaskTagsController(TodoApiClientContext clientContext)
    {
        this.clientContext = clientContext;
    }


    public async Task<IActionResult> Index()
    {
        var tags = await this.clientContext.GetEntitiesAsync<TaskTag>();

        var models = tags?.Select(x => new TaskTagViewModel() { Instance = x }).ToList();

        return this.View(models);
    }

    [HttpPost]
    public async Task<IActionResult> AddTagToTask(TaskTaggingViewModel model)
    {
        if (model.TaskTagId <= 0)
        {
            return this.Redirect(model.ReturnUrl ?? "/");
        }

        var m2M = new TagToTask
        {
            TaskTagId = model.TaskTagId,
            TodoTaskId = model.TodoTaskId,
        };

        await this.clientContext.SaveEntityAsync(m2M);

        return this.Redirect(model.ReturnUrl ?? "/");
    }
}
