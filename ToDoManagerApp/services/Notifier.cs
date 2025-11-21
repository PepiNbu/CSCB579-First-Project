using ToDoManagerApp.interfaces;

namespace ToDoManagerApp.services;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Implementation of INotifier that shows notifications using message boxes.
public static class Notifier
{
    // Notifier that displays messages in a message box.
    public class MessageBoxNotifier : INotifier
    {
        // Method to show a message box with the provided message.
        public void Notify(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}