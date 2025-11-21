using ToDoManagerApp.models;

namespace ToDoManagerApp.interfaces;

public interface ITaskRepository
{
    IEnumerable<ToDoTask> GetAll();
    void Add(ToDoTask task);
    void Remove(Guid id);
    void Update(ToDoTask task);
    void Save();
    void Load();
}