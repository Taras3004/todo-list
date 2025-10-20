using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public abstract class AbstractEditController<TEntity, TViewModel> : Controller
    where TEntity : BaseEntity, new()
    where TViewModel : IReturnUrlViewModel<TEntity>, new()
{
    protected abstract string ViewName { get; }

    private readonly TodoApiClientContext clientContext;

    protected AbstractEditController(TodoApiClientContext clientContext)
    {
        this.clientContext = clientContext;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string returnUrl)
    {
        this.ViewData["title"] = "Create new";

        return await Task.FromResult<IActionResult>(this.View(this.ViewName, new TViewModel()
        {
            Instance = new TEntity(),
            ReturnUrl = returnUrl,
        }));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, string returnUrl)
    {
        this.ViewData["title"] = "Edit";

        var todo = await this.clientContext.GetEntityAsync<TEntity>(id);

        return this.ModelState.IsValid ? this.View(this.ViewName, new TViewModel()
        {
            Instance = todo!,
            ReturnUrl = returnUrl,
        }) : this.NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(this.ViewName, model);
        }

        await this.clientContext.SaveEntityAsync(model.Instance);

        if (!string.IsNullOrEmpty(model.ReturnUrl))
        {
            return this.Redirect(model.ReturnUrl);
        }

        return this.RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Delete(int id, string returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            _ = this.NotFound();
        }
         
        await this.clientContext.DeleteEntityAsync<TEntity>(id);

        if (string.IsNullOrEmpty(returnUrl))
        {
            _ = this.RedirectToAction("Index", "Home");
        }

        return this.Redirect(returnUrl);
    }

    public Task<IActionResult> Cancel(string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
        {
            _ = this.RedirectToAction("Index", "Home");
        }

        return Task.FromResult<IActionResult>(this.Redirect(returnUrl));
    }
}
