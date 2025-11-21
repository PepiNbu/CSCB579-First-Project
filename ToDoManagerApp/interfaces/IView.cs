using ToDoManagerApp.models;

namespace ToDoManagerApp.interfaces;

using System;
using System.Collections.Generic;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Interface for the task management view.
public interface ITaskView
{
    string TitleInput { get; }
    string DescriptionInput { get; }
    string SelectedPriority { get; }
    
    event EventHandler AddTaskRequested;
    event EventHandler RemoveTaskRequested;
    event EventHandler DetailsRequested;
    event EventHandler SaveRequested;
    event EventHandler LoadRequested;
    event EventHandler EditRequested;

    void SetTaskList(IEnumerable<ToDoTask> tasks);
    void ShowMessage(string message, string caption = "Информация");
    void ClearInputs();
    void PopulateInputs(ToDoTask task);

    ToDoTask? SelectedTask { get; }
}