using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;

namespace ToDoManagerApp.services;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Service class for managing tasks, including adding, removing, and retrieving tasks.
public class TaskService(ITaskRepository repository, INotifier notifier)
{
    private readonly ITaskRepository repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly INotifier notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));

    public IEnumerable<ToDoTask> GetAll() => repository.GetAll();

    // Adds a new task after validating the title.
    public void AddTask(string title, string description, string priority)
    {
        Validate(title);
        var task = new ToDoTask(title, description, priority);
        repository.Add(task);
        notifier.Notify($"Задачата '{title}' беше добавена!");
    }

    // Removes a task by its unique identifier.
    public void RemoveTask(Guid id)
    {
        repository.Remove(id);
    }

    // Updates an existing task after validating the title.
    public void Save() => repository.Save();

    // Loads tasks from the repository.
    public void Load() => repository.Load();

    // Validates the task title to ensure it is not empty.
    private static void Validate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
    }
}