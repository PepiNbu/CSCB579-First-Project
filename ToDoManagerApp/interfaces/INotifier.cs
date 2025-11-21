namespace ToDoManagerApp.interfaces;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Interface for notifying users about events or actions.
public interface INotifier
{
    // Method to send a notification with the given message.
    void Notify(string message);
}