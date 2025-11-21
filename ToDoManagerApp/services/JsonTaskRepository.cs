using System.Text.Json;
using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;

namespace ToDoManagerApp.services;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Repository class for managing tasks stored in a JSON file.
public class JsonTaskRepository : ITaskRepository
{
    private readonly string storagePath;
    private readonly List<ToDoTask> tasks = [];

    public JsonTaskRepository(string storagePath)
    {
        this.storagePath = storagePath;
        var dir = Path.GetDirectoryName(storagePath);
        
        // Ensure the directory exists
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Load existing tasks if the file exists
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

    // Add a new task to the repository.
    public void Add(ToDoTask task)
    {
        ArgumentNullException.ThrowIfNull(task);
        tasks.Add(task);
    }

    // Remove a task by its unique identifier.
    public void Remove(Guid id)
    {
        var t = tasks.FirstOrDefault(x => x.Id == id);
        if (t != null)
        {
            tasks.Remove(t);
        }
    }

    // Update an existing task.
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

    // Save the current tasks to the JSON file.
    public void Save()
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(storagePath, json);
    }

    // Load tasks from the JSON file.
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