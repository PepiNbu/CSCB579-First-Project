using ToDoManagerApp.interfaces;
using ToDoManagerApp.models;

namespace ToDoManagerApp.views;

using System;

//@author: Pepi Ivanov Zlatev
//F. Number: F116665

// Main form for the task management application.
public class MainForm : Form, ITaskView
{
    private TextBox? txtTitle;
    private TextBox? txtDescription;
    private ComboBox? cmbPriority;
    private ListBox? lstTasks;
    private Button? btnAdd;
    private Button? btnRemove;
    private Button? btnDetails;
    private Button? btnSave;
    private Button? btnLoad;
    private Button? btnEdit;

    private readonly object[] priorities = ["Нисък", "Нормален", "Висок", "Критичен"];

    public event EventHandler? AddTaskRequested;
    public event EventHandler? RemoveTaskRequested;
    public event EventHandler? DetailsRequested;
    public event EventHandler? SaveRequested;
    public event EventHandler? LoadRequested;
    public event EventHandler? EditRequested;

    public MainForm()
    {
        InitializeComponentCustom();
    }

    // Custom initialization of UI components.
    private void InitializeComponentCustom()
    {
        //Window properties
        Text = "Task Manager";
        Size = new Size(560, 540);
        StartPosition = FormStartPosition.CenterScreen;

        //Task label and textbox
        var lblTitle = new Label { Text = "Заглавие:", Location = new Point(20, 20), AutoSize = true };
        txtTitle = new TextBox { Location = new Point(20, 40), Width = 360 };

        //Priority label and combobox
        var lblPriority = new Label { Text = "Приоритет:", Location = new Point(420, 20), AutoSize = true };
        cmbPriority = new ComboBox
            { Location = new Point(420, 40), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbPriority.Items.AddRange(priorities);
        cmbPriority.SelectedIndex = 1;

        //Description label and textbox
        var lblDesc = new Label { Text = "Описание:", Location = new Point(20, 70), AutoSize = true };
        txtDescription = new TextBox { Location = new Point(20, 90), Width = 500, Height = 80, Multiline = true };

        //Add button
        btnAdd = new Button { Text = "Добави Задача", Location = new Point(20, 190), Width = 500, Height = 30 };
        btnAdd.Click += (_, _) => AddTaskRequested?.Invoke(this, EventArgs.Empty);

        //Edit button
        btnEdit = new Button { Text = "Промени", Location = new Point(120, 420), Width = 100 };
        btnEdit.Click += (_, _) => EditRequested?.Invoke(this, EventArgs.Empty);
        btnEdit.Enabled = false;

        // Load button
        btnLoad = new Button { Text = "Зареди", Location = new Point(320, 420), Width = 120 };
        btnLoad.Click += (_, _) => LoadRequested?.Invoke(this, EventArgs.Empty);

        //Remove, Details, Save
        btnRemove = new Button { Text = "Изтрий", Location = new Point(20, 460), Width = 100 };
        btnRemove.Click += (_, _) => RemoveTaskRequested?.Invoke(this, EventArgs.Empty);
        btnDetails = new Button { Text = "Детайли", Location = new Point(220, 460), Width = 100 };
        btnDetails.Click += (_, _) => DetailsRequested?.Invoke(this, EventArgs.Empty);
        btnSave = new Button { Text = "Запази", Location = new Point(420, 460), Width = 100 };
        btnSave.Click += (_, _) => SaveRequested?.Invoke(this, EventArgs.Empty);

        //Task listbox
        lstTasks = new ListBox { Location = new Point(20, 230), Width = 500, Height = 180 };
        lstTasks.SelectedIndexChanged += (_, _) => { btnEdit!.Enabled = lstTasks.SelectedItem != null; };


        //Adding controls to the form
        Controls.Add(lblTitle);
        Controls.Add(txtTitle);
        Controls.Add(lblPriority);
        Controls.Add(cmbPriority);
        Controls.Add(lblDesc);
        Controls.Add(txtDescription);
        Controls.Add(btnAdd);
        Controls.Add(lstTasks);
        Controls.Add(btnRemove);
        Controls.Add(btnDetails);
        Controls.Add(btnSave);
        Controls.Add(btnLoad);
        Controls.Add(btnEdit);
    }


    public string TitleInput => txtTitle?.Text!;
    public string DescriptionInput => txtDescription!.Text;
    public string SelectedPriority => cmbPriority!.SelectedItem?.ToString() ?? "Нормален";
    
    // Populates the input fields with the details of the specified task.
    public void PopulateInputs(ToDoTask? task)
    {
        if (task == null)
        {
            return;
        }

        txtTitle!.Text = task.Title;
        txtDescription!.Text = task.Description;
        cmbPriority!.SelectedItem = task.Priority;

        // Switch button to save/edit mode
        btnAdd!.Text = "Запази Промяната";
    }

    // Clears the input fields and resets the form to add mode.
    public void ClearInputs()
    {
        txtTitle!.Clear();
        txtDescription!.Clear();
        cmbPriority!.SelectedIndex = 1;
        txtTitle.Focus();

        // Restore add mode label
        btnAdd!.Text = "Добави Задача";
    }
    
    public ToDoTask? SelectedTask => lstTasks!.SelectedItem as ToDoTask;

    // Sets the task list in the ListBox control.
    public void SetTaskList(IEnumerable<ToDoTask> tasks)
    {
        lstTasks!.BeginUpdate();
        lstTasks.Items.Clear();

        foreach (var task in tasks)
        {
            lstTasks.Items.Add(task);
        }

        lstTasks.EndUpdate();
    }

    // Displays a message box with the specified message and caption.
    public void ShowMessage(string message, string caption = "Информация")
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}