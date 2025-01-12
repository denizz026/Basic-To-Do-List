using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private List<ToDoItem> todoItems;
        private ListView listViewTasks;
        private Button btnAdd;
        private Button btnComplete;
        private ComboBox categoryFilter;


        public Form1()
        {
            todoItems = new List<ToDoItem>();
            InitializeComponent();
            SetupCustomComponents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetupCustomComponents()
        {
            // Form ayarları
            this.Size = new Size(800, 600);
            this.Text = "To-Do List Manager";

            // ListView oluşturma
            listViewTasks = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(10, 50),
                Size = new Size(760, 450)
            };

            listViewTasks.Columns.Add("Title", 200);
            listViewTasks.Columns.Add("Due Date", 100);
            listViewTasks.Columns.Add("Category", 100);
            listViewTasks.Columns.Add("Priority", 80);
            listViewTasks.Columns.Add("Status", 80);

            // Category Filter ComboBox
            categoryFilter = new ComboBox
            {
                Location = new Point(10, 20),
                Size = new Size(150, 25)
            };
            categoryFilter.Items.Add("All");
            categoryFilter.SelectedIndex = 0;
            categoryFilter.SelectedIndexChanged += CategoryFilter_SelectedIndexChanged;

            // Buttons
            btnAdd = new Button
            {
                Text = "Add Task",
                Location = new Point(10, 520),
                Size = new Size(100, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            btnComplete = new Button
            {
                Text = "Complete Task",
                Location = new Point(120, 520),
                Size = new Size(100, 30)
            };
            btnComplete.Click += BtnComplete_Click;

            // Kontrolleri forma ekleme
            this.Controls.Add(listViewTasks);
            this.Controls.Add(categoryFilter);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnComplete);
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddTaskForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    todoItems.Add(addForm.TodoItem);
                    RefreshListView();
                    UpdateCategoryFilter();
                }
            }
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > 0)
            {
                var index = listViewTasks.SelectedIndices[0];
                todoItems[index].IsCompleted = !todoItems[index].IsCompleted;
                RefreshListView();
            }
        }

        private void CategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void RefreshListView()
        {
            listViewTasks.Items.Clear();
            var filteredItems = todoItems;

            if (categoryFilter.SelectedItem.ToString() != "All")
            {
                filteredItems = todoItems.Where(x => x.Category == categoryFilter.SelectedItem.ToString()).ToList();
            }

            foreach (var item in filteredItems)
            {
                var listItem = new ListViewItem(item.Title);
                listItem.SubItems.Add(item.DueDate.ToShortDateString());
                listItem.SubItems.Add(item.Category);
                listItem.SubItems.Add(item.Priority.ToString());
                listItem.SubItems.Add(item.IsCompleted ? "Completed" : "Pending");

                if (item.IsCompleted)
                    listItem.ForeColor = Color.Gray;

                listViewTasks.Items.Add(listItem);
            }
        }


        private void UpdateCategoryFilter()
        {
            var categories = todoItems.Select(x => x.Category).Distinct().ToList();
            categoryFilter.Items.Clear();
            categoryFilter.Items.Add("All");
            categoryFilter.Items.AddRange(categories.ToArray());
            categoryFilter.SelectedIndex = 0;
        }

    }
}
