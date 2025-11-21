using ToDoManagerApp.interfaces;

namespace ToDoManagerApp.services;

public static class Notifier
{
    public class MessageBoxNotifier : INotifier
    {
        public void Notify(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}