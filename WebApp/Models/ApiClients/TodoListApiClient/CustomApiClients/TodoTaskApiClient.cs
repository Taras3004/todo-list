using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.CustomApiClients;

public class TodoTaskApiClient : ApiClient<TodoTask>
{
    private readonly ApiClient<TodoTaskPage> taskPageApiClient;

    public TodoTaskApiClient(HttpClient http, string apiRoute, ApiClient<TodoTaskPage> taskPageApiClient) : base(http, apiRoute)
    {
        this.taskPageApiClient = taskPageApiClient;
    }

    protected override async Task CreateAsync(TodoTask entity)
    {
       await base.CreateAsync(entity);

       await this.taskPageApiClient.SaveAsync(new TodoTaskPage()
       {
           TodoTaskId = entity.Id
       });
    }

    public override async Task DeleteAsync(int id)
    {
        var page = this.taskPageApiClient.GetAllAsync().Result!.FirstOrDefault(x => x.TodoTaskId == id);

        await this.taskPageApiClient.DeleteAsync(page!.Id);

        await base.DeleteAsync(id);
    }
}
