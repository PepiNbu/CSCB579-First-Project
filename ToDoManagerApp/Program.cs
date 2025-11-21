using ToDoManagerApp.interfaces;
using ToDoManagerApp.presenters;
using ToDoManagerApp.services;
using ToDoManagerApp.views;

namespace ToDoManagerApp;

internal static class Program
{
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var resourcesDir = Path.Combine("resources");
        Directory.CreateDirectory(resourcesDir);

        var storage = Path.Combine(resourcesDir, "tasks.json");

        ITaskRepository repo = new JsonTaskRepository(storage);
        INotifier notifier = new Notifier.MessageBoxNotifier();
        var service = new TaskService(repo, notifier);

        var mainForm = new MainForm();
        _ = new TaskPresenter(mainForm, service);

        Application.Run(mainForm);
    }
}