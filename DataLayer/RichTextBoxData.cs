using BoDoiApp.Resources;
using System.Data.SQLite;

namespace BoDoiApp.DataLayer
{
    public class RichTextBoxData
    {
        public void CreatTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS RichTextBoxData (
                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                           UserId TEXT NOT NULL,
                           Section TEXT NOT NULL,
                           Content TEXT
                       )";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // ===== CHECK EXIST =====
        public bool Exists(string userId, string section)
        {
            string sql = @"SELECT COUNT(*) 
                           FROM RichTextBoxData
                           WHERE UserId=@UserId AND Section=@Section";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);

                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // ===== INSERT =====
        public void AddData(string userId, string content, string section)
        {
            string sql = @"INSERT INTO RichTextBoxData (UserId, Section, Content) 
                           VALUES (@UserId, @Section, @Content)";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }
        }

        // ===== UPDATE =====
        public void UpdateData(string userId, string content, string section)
        {
            string sql = @"UPDATE RichTextBoxData
                           SET Content=@Content
                           WHERE UserId=@UserId AND Section=@Section";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);
                    command.ExecuteNonQuery();
                }
            }
        }

        // ===== SAVE OR UPDATE =====
        public void SaveOrUpdate(string userId, string content, string section)
        {
            if (Exists(userId, section))
                UpdateData(userId, content, section);
            else
                AddData(userId, content, section);
        }

        // ===== LOAD =====
        public string LoadDataFromDatabase(string userId, string section)
        {
            string sql = @"SELECT Content
                           FROM RichTextBoxData
                           WHERE UserId=@UserId AND Section=@Section";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);

                    var result = command.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
        }
    }
}