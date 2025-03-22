using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_Group_1
{
    internal class AddStudentData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\maina newton\OneDrive\Documents\db_student_data.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public int ID { set; get; }
        public string StudentID { set; get; }
        public string StudentName { set; get; }
        public string StudentGender { set; get; }
        public string StudentAddress { set; get; }
        public string StudentGrade { set; get; }
        public string StudentCourse { set; get; }
        public string StudentImage { set; get; }
        public string Status { set; get; }
        public DateTime DateInsert { get; set; }

        public List<AddStudentData> studentData()
        {
            List<AddStudentData> listData = new List<AddStudentData>();
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    // Join the students and courses table to get the course name
                    string sql = "SELECT s.id, s.student_id, s.student_name, s.student_gender, " +
                                 "s.student_address, s.student_grade, s.course_name, " +
                                 "s.student_image, s.student_status, s.date_insert, " +
                                 "c.course_name " +
                                 "FROM students s " +
                                 "JOIN courses c ON s.course_name = c.course_id " +
                                 "WHERE s.date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AddStudentData addSD = new AddStudentData();
                            addSD.ID = (int)reader["id"];
                            addSD.StudentID = reader["student_id"].ToString();
                            addSD.StudentName = reader["student_name"].ToString();
                            addSD.StudentGender = reader["student_gender"].ToString();
                            addSD.StudentAddress = reader["student_address"].ToString();
                            addSD.StudentGrade = reader["student_grade"].ToString();
                            addSD.StudentCourse = reader["course_name"].ToString();  // Display course_name
                            addSD.StudentImage = reader["student_image"].ToString();
                            addSD.Status = reader["student_status"].ToString();
                            addSD.DateInsert = reader.GetDateTime(reader.GetOrdinal("date_insert"));


                            listData.Add(addSD);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting Database: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }

        public List<(int CourseID, string CourseName)> GetCourses()
        {
            List<(int, string)> courses = new List<(int, string)>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string sql = "SELECT course_id, course_name FROM courses";  

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int courseId = (int)reader["course_id"];
                            string courseName = reader["course_name"].ToString();
                            courses.Add((courseId, courseName));  
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving courses: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
            return courses;
        }



        public List<AddStudentData> dashboardStudentData()
        {
            List<AddStudentData> listData = new List<AddStudentData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    DateTime today = DateTime.Today;
                    string sql = "SELECT * FROM students WHERE date_insert = @dateInsert " +
                        "AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        cmd.Parameters.AddWithValue("@dateInsert", today);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AddStudentData addSD = new AddStudentData();
                            addSD.ID = (int)reader["id"];
                            addSD.StudentID = reader["student_id"].ToString();
                            addSD.StudentName = reader["student_name"].ToString();
                            addSD.StudentGender = reader["student_gender"].ToString();
                            addSD.StudentAddress = reader["student_address"].ToString();
                            addSD.StudentGrade = reader["student_grade"].ToString();
                            addSD.StudentCourse = reader["course_name"].ToString();
                            addSD.StudentImage = reader["student_image"].ToString();
                            addSD.Status = reader["student_status"].ToString();
                            addSD.DateInsert = reader.GetDateTime(reader.GetOrdinal("date_insert"));

                            listData.Add(addSD);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }


    }
}
