using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Management_System_Group_1
{
    public partial class DashboardForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\maina newton\OneDrive\Documents\db_student_data.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public DashboardForm()
        {
            InitializeComponent();

            displayTotalES();
            displayTotalAC();
            displayTotalGS();

            displayStudentsEnrolledToday();
        }

        public void displayTotalES()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM students WHERE student_status = 'Enrolled' AND ISNULL(date_delete, '') = ''";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        int tempES = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);

                        total_ES.Text = tempES.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to the Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalAC()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(course_id) FROM courses";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        int totalCourses = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);

                        total_AC.Text = totalCourses.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to the Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalGS()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM students WHERE student_status = 'Graduated'";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        int tempGS = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        total_GS.Text = tempGS.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to the Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }


        public void displayStudentsEnrolledToday() 
        {
            AddStudentData asData = new AddStudentData();

            dataGridView_DB.DataSource = asData.dashboardStudentData();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void total_ES_Click(object sender, EventArgs e)
        {

        }
    }
}
