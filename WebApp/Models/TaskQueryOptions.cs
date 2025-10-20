namespace WebApp.Models;

public class TaskQueryOptions
{
    public string? FilterStatus { get; set; } = null;

    public string? SortBy { get; set; } = null;

    public int SelectedTagId { get; set; } = 0;
}
