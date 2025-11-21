using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;
using ToDoManagerApp.services;

namespace ToDoManagerApp.presenters;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Presenter class for managing task-related interactions between the view and the service.
public class TaskPresenter
{
    private ToDoTask? taskBeingEdited;
    private readonly ITaskView view;
    private readonly TaskService service;

    public TaskPresenter(ITaskView view, TaskService service)
    {
        this.view = view ?? throw new ArgumentNullException(nameof(view));
        this.service = service ?? throw new ArgumentNullException(nameof(service));

        this.view.EditRequested += OnEditRequested;
        this.view.AddTaskRequested += OnAddTaskRequested;
        this.view.RemoveTaskRequested += OnRemoveTaskRequested;
        this.view.DetailsRequested += OnDetailsRequested;
        this.view.SaveRequested += OnSaveRequested;
        this.view.LoadRequested += OnLoadRequested;

        RefreshView();
    }

    // Refreshes the task list in the view.
    private void RefreshView()
    {
        var tasks = service.GetAll();
        view.SetTaskList(tasks);
    }

    // Handles the edit request event.
    private void OnEditRequested(object? sender, EventArgs e)
    {
        var selected = view.SelectedTask;
        if (selected == null)
        {
            view.ShowMessage("Моля, изберете задача за редакция!");
            return;
        }

        taskBeingEdited = selected;
        view.PopulateInputs(selected);
    }

    // Handles the add task request event.
    private void OnAddTaskRequested(object? sender, EventArgs e)
    {
        if (taskBeingEdited == null)
        {
            service.AddTask(view.TitleInput, view.DescriptionInput, view.SelectedPriority);
            service.Save();
            view.ShowMessage("Задачата е добавена!");
        }
        else
        {
            taskBeingEdited.Title = view.TitleInput;
            taskBeingEdited.Description = view.DescriptionInput;
            taskBeingEdited.Priority = view.SelectedPriority;

            service.Save();
            view.ShowMessage("Промените са запазени!");

            // exit edit mode
            taskBeingEdited = null;
        }

        RefreshView();
        view.ClearInputs();
    }
    
    // Handles the remove task request event.
    private void OnRemoveTaskRequested(object? sender, EventArgs e)
    {
        var selected = view.SelectedTask;
        if (selected == null)
        {
            view.ShowMessage("Моля, изберете задача за изтриване.");
            return;
        }

        service.RemoveTask(selected.Id);
        RefreshView();
    }

    // Handles the details request event.
    private void OnDetailsRequested(object? sender, EventArgs e)
    {
        var selected = view.SelectedTask;
        if (selected == null)
        {
            view.ShowMessage("Моля, изберете задача.");
            return;
        }

        view.ShowMessage(selected.GetFullInfo(), "Детайли за задача");
    }

    // Handles the save request event.
    private void OnSaveRequested(object? sender, EventArgs e)
    {
        try
        {
            service.Save();
            view.ShowMessage("Записано успешно.");
        }
        catch (Exception ex)
        {
            view.ShowMessage("Грешка при запис: " + ex.Message);
        }
    }

    // Handles the load request event.
    private void OnLoadRequested(object? sender, EventArgs e)
    {
        try
        {
            service.Load();
            RefreshView();
            view.ShowMessage("Заредено успешно.");
        }
        catch (Exception ex)
        {
            view.ShowMessage("Грешка при зареждане: " + ex.Message);
        }
    }
}