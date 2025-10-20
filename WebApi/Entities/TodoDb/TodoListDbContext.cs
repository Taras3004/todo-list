using Microsoft.EntityFrameworkCore;
using WebApi.Entities.UsersDb;

namespace WebApi.Entities.TodoDb;

public class TodoListDbContext : DbContext
{
    public DbSet<TodoList> Todos { get; set; }

    public DbSet<TodoTask> Tasks { get; set; }

    public DbSet<TaskTag> TaskTags { get; set; }

    public DbSet<TodoTaskPage> TaskPages { get; set; }

    public DbSet<TagToTask> TagToTask { get; set; }

    public DbSet<TaskComment> TaskComments { get; set; }

    public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<ApplicationUser>();
    }
}
