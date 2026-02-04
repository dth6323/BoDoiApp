using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoDoiApp.DataLayer
{
    public class RichTextBoxData
    {
        public void AddData(string userId,string content,string section)
        {
            string sql = @"INSERT INTO RichTextBoxData (UserId, Section, Content) 
                           VALUES (@UserId, @Section, @Content)";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }
        }

        public string LoadDataFromDatabase(string userId,string section)
        {
            string sql = @"SELECT Content FROM RichTextBoxData 
                           WHERE UserId = @UserId AND Section = @Section";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
                {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);
                    var result = command.ExecuteScalar();
                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }
        
        public void UpdateData(string userId,string content,string section)
        {
            string sql = @"UPDATE RichTextBoxData 
                           SET Content = @Content 
                           WHERE UserId = @UserId AND Section = @Section";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
                {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
