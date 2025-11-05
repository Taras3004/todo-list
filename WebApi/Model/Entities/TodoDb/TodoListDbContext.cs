using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.UsersDb;

namespace WebApi.Model.Entities.TodoDb;

public class TodoListDbContext(DbContextOptions<TodoListDbContext> options) : DbContext(options)
{
    public DbSet<TodoList> Todos { get; set; }

    public DbSet<TodoTask> Tasks { get; set; }

    public DbSet<TaskTag> TaskTags { get; set; }

    public DbSet<TodoTaskPage> TaskPages { get; set; }

    public DbSet<TagToTask> TagToTask { get; set; }

    public DbSet<TaskComment> TaskComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<ApplicationUser>();

        modelBuilder.Entity<TodoTaskPage>()
            .HasOne(x => x.Task)
            .WithOne()
            .HasForeignKey<TodoTaskPage>(x => x.TodoTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskComment>()
            .HasOne(x => x.Task)
            .WithMany()
            .HasForeignKey(x => x.TodoTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TagToTask>()
            .HasOne(x => x.Task)
            .WithMany()
            .HasForeignKey(x => x.TodoTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TagToTask>()
            .HasOne(x => x.Tag)
            .WithMany()
            .HasForeignKey(x => x.TaskTagId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TodoTask>()
            .HasOne(x => x.TodoList)
            .WithMany()
            .HasForeignKey(x => x.TodoListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
