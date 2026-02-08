using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class BaoDamQuanYData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public bool ThemThongTin(string noiDung, string loai)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO baodamsinhhoat (noidung, loai, user)
                                   VALUES (@noidung, @loai, @user)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@noidung", noiDung);
                        command.Parameters.AddWithValue("@loai", loai);
                        command.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi thêm thông tin: {ex.Message}");
                return false;
            }
        }

        public bool CapNhatThongTin(string noiDung, string loai)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE baodamsinhhoat 
                                   SET noidung = @noidung
                                   WHERE user = @user AND loai = @loai";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@noidung", noiDung);
                        command.Parameters.AddWithValue("@loai", loai);
                        command.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi cập nhật thông tin: {ex.Message}");
                return false;
            }
        }

        public string LayThongTin(string loai)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT noidung FROM baodamsinhhoat WHERE user = @user AND loai = @loai";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@loai", loai);
                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
