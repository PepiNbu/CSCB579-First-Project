using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;

namespace ToDoManagerApp.services;

public class TaskService(ITaskRepository repository, INotifier notifier)
{
    private readonly ITaskRepository repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly INotifier notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));

    public IEnumerable<ToDoTask> GetAll() => repository.GetAll();

    public void AddTask(string title, string description, string priority)
    {
        Validate(title);
        var task = new ToDoTask(title, description, priority);
        repository.Add(task);
        notifier.Notify($"Задачата '{title}' беше добавена!");
    }

    public void RemoveTask(Guid id)
    {
        repository.Remove(id);
    }

    public void Save() => repository.Save();

    public void Load() => repository.Load();

    private static void Validate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
    }
}