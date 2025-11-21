using ToDoManagerApp.models;

namespace ToDoManagerApp.interfaces;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Interface for managing task data storage and retrieval.
public interface ITaskRepository
{
    IEnumerable<ToDoTask> GetAll();
    void Add(ToDoTask task);
    void Remove(Guid id);
    void Update(ToDoTask task);
    void Save();
    void Load();
}