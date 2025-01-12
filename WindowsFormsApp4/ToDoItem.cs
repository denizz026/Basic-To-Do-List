using System;

namespace WindowsFormsApp4
{
    public class ToDoItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}