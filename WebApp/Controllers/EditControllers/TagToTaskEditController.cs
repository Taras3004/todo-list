using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public class TagToTaskEditController : AbstractEditController<TagToTask, TaskTaggingViewModel>
{
    private readonly TodoApiClientContext clientContext;

    public TagToTaskEditController(TodoApiClientContext clientContext) : base(clientContext)
    {
        this.clientContext = clientContext;
    }

    protected override string ViewName { get; } = "";

    public async Task<IActionResult> DeleteTagToTask(int tagId, int taskId, string returnUrl)
    {
        var tagToTaskList = await this.clientContext.GetEntitiesAsync<TagToTask>();

        if (tagToTaskList == null || !this.ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                _ = this.RedirectToAction("Index", "Home");
            }

            return this.Redirect(returnUrl);
        }

        var tagToTask = tagToTaskList
            .Where(x => x.TaskTagId == tagId)
            .FirstOrDefault(x => x.TodoTaskId == taskId);

        if (tagToTask == null)
        {
            return this.NotFound();
        }

        await this.clientContext.DeleteEntityAsync<TagToTask>(tagToTask.Id);

        if (string.IsNullOrEmpty(returnUrl))
        {
            _ = this.RedirectToAction("Index", "Home");
        }

        return this.Redirect(returnUrl);
    }
}
