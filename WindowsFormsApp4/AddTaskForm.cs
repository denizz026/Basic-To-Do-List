using System.Drawing;
using System.Windows.Forms;
using System;

namespace WindowsFormsApp4
{
    public class AddTaskForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpDueDate;
        private TextBox txtCategory;
        private ComboBox cboPriority;
        private Button btnSave;

        public ToDoItem TodoItem { get; private set; }

        public AddTaskForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(400, 300);
            this.Text = "Add New Task";

            var lblTitle = new Label
            {
                Text = "Title:",
                Location = new Point(10, 20)
            };
            txtTitle = new TextBox
            {
                Location = new Point(100, 20),
                Size = new Size(250, 20)
            };

            var lblDescription = new Label
            {
                Text = "Description:",
                Location = new Point(10, 50)
            };
            txtDescription = new TextBox
            {
                Location = new Point(100, 50),
                Size = new Size(250, 60),
                Multiline = true
            };

            var lblDueDate = new Label
            {
                Text = "Due Date:",
                Location = new Point(10, 120)
            };
            dtpDueDate = new DateTimePicker
            {
                Location = new Point(100, 120),
                Size = new Size(250, 20)
            };

            var lblCategory = new Label
            {
                Text = "Category:",
                Location = new Point(10, 150)
            };
            txtCategory = new TextBox
            {
                Location = new Point(100, 150),
                Size = new Size(250, 20)
            };

            var lblPriority = new Label
            {
                Text = "Priority:",
                Location = new Point(10, 180)
            };
            cboPriority = new ComboBox
            {
                Location = new Point(100, 180),
                Size = new Size(250, 20)
            };
            cboPriority.Items.AddRange(Enum.GetNames(typeof(Priority)));
            cboPriority.SelectedIndex = 0;

            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(150, 220),
                Size = new Size(100, 30)
            };
            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[] {
                lblTitle, txtTitle,
                lblDescription, txtDescription,
                lblDueDate, dtpDueDate,
                lblCategory, txtCategory,
                lblPriority, cboPriority,
                btnSave
            });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required!");
                return;
            }

            TodoItem = new ToDoItem
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                DueDate = dtpDueDate.Value,
                Category = txtCategory.Text,
                Priority = (Priority)Enum.Parse(typeof(Priority), cboPriority.SelectedItem.ToString()),
                IsCompleted = false
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}