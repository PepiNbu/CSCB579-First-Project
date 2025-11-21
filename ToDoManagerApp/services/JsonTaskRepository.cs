using System.Text.Json;
using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;

namespace ToDoManagerApp.services;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string storagePath;
    private readonly List<ToDoTask> tasks = [];

    public JsonTaskRepository(string storagePath)
    {
        this.storagePath = storagePath;
        var dir = Path.GetDirectoryName(storagePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        if (!File.Exists(storagePath))
        {
            return;
        }

        try
        {
            Load();
        }
        catch
        {
            /* ignore load errors at construction */
        }
    }

    public IEnumerable<ToDoTask> GetAll() => tasks.ToList();

    public void Add(ToDoTask task)
    {
        ArgumentNullException.ThrowIfNull(task);
        tasks.Add(task);
    }

    public void Remove(Guid id)
    {
        var t = tasks.FirstOrDefault(x => x.Id == id);
        if (t != null)
        {
            tasks.Remove(t);
        }
    }

    public void Update(ToDoTask task)
    {
        var existing = tasks.FirstOrDefault(x => x.Id == task.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Task not found");
        }

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.Priority = task.Priority;
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(storagePath, json);
    }

    public void Load()
    {
        if (!File.Exists(storagePath))
        {
            return;
        }

        var json = File.ReadAllText(storagePath);
        var loaded = JsonSerializer.Deserialize<List<ToDoTask>>(json);
        tasks.Clear();
        if (loaded != null) tasks.AddRange(loaded);
    }
}