using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_Group_1
{
    internal class AddCourseData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\maina newton\OneDrive\Documents\db_student_data.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
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

        public bool AddCourse(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                throw new ArgumentException("Course name cannot be empty.");
            }

            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "INSERT INTO courses (course_name) VALUES (@CourseName)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding course: " + ex.Message);
                return false;
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }


    }
}
