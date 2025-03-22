using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_Group_1
{
    public partial class AddCoursesForm : Form
    {
        private AddCourseData courseData;

        public AddCoursesForm()
        {
            InitializeComponent();
            courseData = new AddCourseData();
            LoadCourses();
        }

        // Load existing courses into DataGridView
        private void LoadCourses()
        {
            try
            {
                List<(int CourseID, string CourseName)> courses = courseData.GetCourses();
                DataTable dt = new DataTable();
                dt.Columns.Add("Course ID", typeof(int));
                dt.Columns.Add("Course Name", typeof(string));

                foreach (var course in courses)
                {
                    dt.Rows.Add(course.CourseID, course.CourseName);
                }

                coursedataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add course to database when button is clicked
        private void course_add_Btn_Click(object sender, EventArgs e)
        {
            string newCourseName = course_name.Text.Trim();

            if (string.IsNullOrEmpty(newCourseName))
            {
                MessageBox.Show("Please enter a course name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool isAdded = courseData.AddCourse(newCourseName);
                if (isAdded)
                {
                    MessageBox.Show($"Course '{newCourseName}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    course_name.Clear();
                    LoadCourses(); 
                }
                else
                {
                    MessageBox.Show("Failed to add course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void course_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCoursesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
