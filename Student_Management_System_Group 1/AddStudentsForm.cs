using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Student_Management_System_Group_1
{
    public partial class AddStudentsForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\maina newton\OneDrive\Documents\db_student_data.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public AddStudentsForm()
        {
            InitializeComponent();
            displayStudentData();
        }

        public void displayStudentData()
        {
            AddStudentData adData = new AddStudentData();

            studentdataGridView.DataSource = adData.studentData();
        }

        private void student_add_Btn_Click(object sender, EventArgs e)
        {
            if (student_id.Text == "" || student_name.Text == "" || student_gender.Text == "" ||
                student_address.Text == "" || student_grade.Text == "" || course_name.Text == "" ||
                student_status.Text == "" || student_image.Image == null || imagePath == null)
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!IsValidEmail(student_address.Text.Trim()))
                {
                    MessageBox.Show("Please provide a valid email address.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        // Check if the student ID already exists
                        string checkStudentID = "SELECT COUNT(*) FROM students WHERE student_id = @studentID";
                        using (SqlCommand checkSID = new SqlCommand(checkStudentID, connect))
                        {
                            checkSID.Parameters.AddWithValue("@studentID", student_id.Text.Trim());
                            int count = (int)checkSID.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show("Student ID: " + student_id.Text.Trim() + " already exists",
                                                "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;

                                string insertData = "INSERT INTO students (student_id, student_name, student_gender, student_address, student_grade, course_name, student_image, student_status, date_insert) " +
                                                    "VALUES (@studentID, @studentName, @studentGender, @studentAddress, @studentGrade, @courseID, @studentImage, @studentStatus, @dateInsert)";

                                // Save image to your directory
                                string path = Path.Combine(@"C:\Users\maina newton\source\repos\Student_Management_System_Group 1\Student_Management_System_Group 1\Student_Directory", student_id.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(imagePath, path, true);

                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@studentID", student_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@studentName", student_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@studentGender", student_gender.Text.Trim());
                                    cmd.Parameters.AddWithValue("@studentAddress", student_address.Text.Trim());
                                    cmd.Parameters.AddWithValue("@studentGrade", student_grade.Text.Trim());
                                    cmd.Parameters.AddWithValue("@courseID", selectedCourseId); // Use selectedCourseId
                                    cmd.Parameters.AddWithValue("@studentImage", path);
                                    cmd.Parameters.AddWithValue("@studentStatus", student_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dateInsert", today.ToString("yyyy-MM-dd"));

                                    cmd.ExecuteNonQuery();

                                    displayStudentData();
                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clearFields();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                var regex = new Regex(emailPattern);
                if (!regex.IsMatch(email))
                {
                    return false;
                }
                // MX record validation
                var domain = email.Split('@')[1];
                var dnsInfo = Dns.GetHostEntry(domain);
                if (dnsInfo != null && dnsInfo.AddressList.Length > 0)
                {
                    foreach (var ipAddress in dnsInfo.AddressList)
                    {
                        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return true; 
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void clearFields()
        {
            student_id.Text = "";
            student_name.Text = "";
            student_gender.SelectedIndex = -1;
            student_address.Text = "";
            student_grade.SelectedIndex = -1;
            course_name.SelectedIndex = -1;
            student_status.SelectedIndex = -1;
            student_image.Image = null;
            imagePath = "";
        }

        public string imagePath;

        private void student_import_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image files (*.jpg; *.png)|*.jpg;*.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                imagePath = open.FileName;
                student_image.ImageLocation = imagePath;
            }

        }


        private void LoadCourses()
        {
            AddStudentData addStudentData = new AddStudentData();
            var courses = addStudentData.GetCourses();

            course_name.Items.Clear();
            foreach (var course in courses)
            {
                course_name.Items.Add(course.CourseName);  
            }
            if (course_name.Items.Count > 0)
            {
                course_name.SelectedIndex = 0; 
            }
        }

        private int selectedCourseId = 0; 

        private void course_name_Selected(object sender, EventArgs e)
        {
            if (course_name.SelectedIndex != -1)
            {
                AddStudentData addStudentData = new AddStudentData();
                var courses = addStudentData.GetCourses();
                var selectedCourse = courses[course_name.SelectedIndex];
                selectedCourseId = selectedCourse.CourseID;
            }
        }



        private void AddStudentsForm_Load(object sender, EventArgs e)
        {
            LoadCourses(); 
        }

        private void studentdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = studentdataGridView.Rows[e.RowIndex];
                student_id.Text = row.Cells[1].Value.ToString();
                student_name.Text = row.Cells[2].Value.ToString();
                student_gender.Text = row.Cells[3].Value.ToString();
                student_address.Text = row.Cells[4].Value.ToString();
                student_grade.Text = row.Cells[5].Value.ToString();
                course_name.Text = row.Cells[6].Value.ToString();

                imagePath = row.Cells[5].Value.ToString();

                string imageData = row.Cells[7].Value.ToString();

                if (imageData != null && imageData.Length > 0)
                {
                    student_image.Image = Image.FromFile(imageData);
                }
                else
                {
                    student_image.Image = null;
                }

                student_status.Text = row.Cells[8].Value.ToString();

            }
        }

        private void student_delete_Btn_Click(object sender, EventArgs e)
        {
            if (student_id.Text == "")
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to Delete Student ID: "
                        + student_id.Text + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;

                            string deleteData = "UPDATE students SET date_delete = @dateDelete " +
                                                "WHERE student_id = @studentID";

                            using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                            {
                                // Use DateTime object directly
                                cmd.Parameters.AddWithValue("@dateDelete", today);
                                cmd.Parameters.AddWithValue("@studentID", student_id.Text.Trim());

                                cmd.ExecuteNonQuery();

                                displayStudentData();

                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void student_update_Btn_Click(object sender, EventArgs e)
        {
            if (student_id.Text == ""
                || student_name.Text == ""
                || student_gender.Text == ""
                || student_address.Text == ""
                || student_grade.Text == ""
                || course_name.Text == ""
                || student_status.Text == ""
                || student_image.Image == null
                || imagePath == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        DialogResult check = MessageBox.Show("Are you sure you want to Update Student ID: "
                            + student_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (check == DialogResult.Yes)
                        {
                            DateTime today = DateTime.Today;

                            string updateData = "UPDATE students SET student_name = @studentName, " +
                                "student_gender = @studentGender, student_address = @studentAddress, " +
                                "student_grade = @studentGrade, course_name = @courseID, " +
                                "student_status = @studentStatus, date_update = @dateUpdate " +
                                "WHERE student_id = @studentID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@studentName", student_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@studentGender", student_gender.Text.Trim());
                                cmd.Parameters.AddWithValue("@studentAddress", student_address.Text.Trim());
                                cmd.Parameters.AddWithValue("@studentGrade", student_grade.Text.Trim());
                                cmd.Parameters.AddWithValue("@courseID", course_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@studentStatus", student_status.Text.Trim());
                                cmd.Parameters.AddWithValue("@dateUpdate", today.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@studentID", student_id.Text.Trim());

                                cmd.ExecuteNonQuery();

                                displayStudentData();

                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void student_clear_Btn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
