namespace ToDoManagerApp.models;

using System;
using System.Text.Json.Serialization;

[method: JsonConstructor]
public class ToDoTask(Guid id, string title, string description, string priority)
{
    public Guid Id { get; private set; } = id;
    public string Title { get; internal set; } = title;
    public string Description { get; internal set; } = description;
    public string Priority { get; internal set; } = priority;
    private DateTime CreatedAt { get; } = DateTime.Now;

    public ToDoTask(string title, string description, string priority) : this(Guid.NewGuid(), title, description, priority)
    {
    }

    public string GetFullInfo()
    {
        return $"Заглавие: {Title}\nПриоритет: {Priority}\nДата: {CreatedAt}\nОписание: {Description}";
    }

    public override string ToString()
    {
        return $"[{Priority}] {Title} ({CreatedAt.ToShortTimeString()})";
    }
}