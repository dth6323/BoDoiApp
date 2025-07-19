using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class User
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public User()
        {
        }


        public bool Register(string username, string password, string fullName)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Users (Username, Password, FullName) VALUES (@username, @password, @fullName)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", HashPassword(password));
                        command.Parameters.AddWithValue("@fullName", fullName);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                    MessageBox.Show("Tên người dùng đã tồn tại!");
                    return false;
                throw;
            }
        }

        public bool Login(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Password FROM Users WHERE Username = @username";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader.GetString(0);
                            return VerifyPassword(password, storedPassword);
                        }
                        return false;
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            // Ở đây nên dùng thư viện mã hóa mật khẩu như BCrypt
            // Ví dụ đơn giản, chỉ dùng SHA256
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            string hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
