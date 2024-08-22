using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class StudentManager
    {
        public static List<Student> Students;

        public StudentManager()
        {
        }

        static StudentManager()
        {
            Students = new List<Student>();
        }
        public static void AddStudent(Student student)
        {
            DbConnection dbConnection = new DbConnection();

            string query = "INSERT INTO students (cbno, name, gender) VALUES (@CBNO, @Name, @Gender)";
            MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

            cmd.Parameters.AddWithValue("@CBNO", student.GetCBNO());
            cmd.Parameters.AddWithValue("@Name", student.GetName());
            cmd.Parameters.AddWithValue("@Gender", student.GetGender());

            try
            {
                dbConnection.OpenConnection();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

        }

        public void GetAllStudents()
        {
            DbConnection dbConnection = new DbConnection();
            string query = "SELECT * FROM students";
            MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

            try
            {
                dbConnection.OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string cbno = reader["cbno"].ToString();
                    string name = reader["name"].ToString();
                    string gender = reader["gender"].ToString();

                    // Show each student's details in a MessageBox
                    MessageBox.Show($"CBNO: {cbno}\nName: {name}\nGender: {gender}", "Student Details");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public static string[] ToList(Student student)
        {
            string[] TempStudInfo = new string[3] { student.GetCBNO(),student.GetName(),student.GetGender() };
            return TempStudInfo;
        }
    }
}
