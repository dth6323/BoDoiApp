using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class QuanSoChienDauData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public QuanSoChienDauData()
        {
        }

        public bool ThemThongTin(string phienhieudonvi, string phdv1, string phdv2, string phdv3, string phdv4, string phdv5,
            string quansochiendau, string qscd1, string qscd2, string qscd3, string qscd4,string qscd5)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO quansochiendau (phienhieudonvi, phdv1, phdv2, phdv3, phdv4, phdcv5,
                                  quansochiendau, qscd1, qscd2, qscd3, qscd4,qscd5, User) 
                                  VALUES (@phienhieudonvi, @phdv1, @phdv2, @phdv3, @phdv4, @phdv5,
                                  @quansochiendau, @qscd1, @qscd2, @qscd3, @qscd4,@qscd5, @User)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@phienhieudonvi", phienhieudonvi ?? "");
                        command.Parameters.AddWithValue("@phdv1", phdv1 ?? "");
                        command.Parameters.AddWithValue("@phdv2", phdv2 ?? "");
                        command.Parameters.AddWithValue("@phdv3", phdv3 ?? "");
                        command.Parameters.AddWithValue("@phdv4", phdv4 ?? "");
                        command.Parameters.AddWithValue("@phdv5", phdv5 ?? "");
                        command.Parameters.AddWithValue("@quansochiendau", quansochiendau ?? "");
                        command.Parameters.AddWithValue("@qscd1", qscd1 ?? "");
                        command.Parameters.AddWithValue("@qscd2", qscd2 ?? "");
                        command.Parameters.AddWithValue("@qscd3", qscd3 ?? "");
                        command.Parameters.AddWithValue("@qscd4", qscd4 ?? "");
                        command.Parameters.AddWithValue("@qscd5", qscd5 ?? "");
                        command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi thêm thông tin: {ex.Message}\nError Code: {ex.ErrorCode}");
                return false;
            }
        }

        public bool CapNhatThongTin(string phienhieudonvi, string phdv1, string phdv2, string phdv3, string phdv4, string phdv5,
            string quansochiendau, string qscd1, string qscd2, string qscd3, string qscd4, string qscd5)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE quansochiendau 
                                  SET phienhieudonvi = @phienhieudonvi,
                                      phdv1 = @phdv1,
                                      phdv2 = @phdv2,
                                      phdv3 = @phdv3,
                                      phdv4 = @phdv4,
                                      phdv5 = @phdv5,
                                      quansochiendau = @quansochiendau,
                                      qscd1 = @qscd1,
                                      qscd2 = @qscd2,
                                      qscd3 = @qscd3,
                                      qscd4 = @qscd4,
                                      scd5 = @qscd5
                                  WHERE  User = @User";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@phienhieudonvi", phienhieudonvi ?? "");
                        command.Parameters.AddWithValue("@phdv1", phdv1 ?? "");
                        command.Parameters.AddWithValue("@phdv2", phdv2 ?? "");
                        command.Parameters.AddWithValue("@phdv3", phdv3 ?? "");
                        command.Parameters.AddWithValue("@phdv4", phdv4 ?? "");
                        command.Parameters.AddWithValue("@phdv5", phdv5 ?? "");
                        command.Parameters.AddWithValue("@quansochiendau", quansochiendau ?? "");
                        command.Parameters.AddWithValue("@qscd1", qscd1 ?? "");
                        command.Parameters.AddWithValue("@qscd2", qscd2 ?? "");
                        command.Parameters.AddWithValue("@qscd3", qscd3 ?? "");
                        command.Parameters.AddWithValue("@qscd4", qscd4 ?? "");
                        command.Parameters.AddWithValue("@qscd5", qscd5 ?? "");
                        command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật thông tin: {ex.Message}");
                return false;
            }
        }

        public DataTable LayThongTin()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM quansochiendau WHERE User = @User";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lấy thông tin: {ex.Message}");
                return null;
            }
            return dt;
        }
    }
}