using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class VatChatNguoiDungData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public VatChatNguoiDungData()
        {
        }
        public void ThemHangLoat(DataGridView dgv)
        {
            int j = 0;
            for (int i = 2; i < dgv.Rows.Count; i++)
            {
                if (i == 7 || i == 14) continue;
                j++;
                string toand = dgv.Rows[i].Cells[3].Value?.ToString() ?? "";
                string gc = dgv.Rows[i].Cells[4].Value?.ToString() ?? "";
                ThemThongTin(toand, gc, j);
            }
        }
        public void SuaHangLoat(DataGridView dgv)
        {
            int j = 0;
            for (int i = 2; i < dgv.Rows.Count; i++)
            {
                if (i == 7 || i == 14) continue;
                j++;
                string toand = dgv.Rows[i].Cells[3].Value?.ToString() ?? "";
                string gc = dgv.Rows[i].Cells[4].Value?.ToString() ?? "";
                CapNhatThongTin(j, toand, gc);
            }
        }
        public bool ThemThongTin(string soLuong, string ghiChu,int vcId)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO VatChatNguoiDung (UserId, SoLuong, GhiChu,vcId) 
                                  VALUES (@UserId, @SoLuong, @GhiChu,@vcId)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@SoLuong", soLuong ?? "");
                        command.Parameters.AddWithValue("@GhiChu", ghiChu ?? "");
                        command.Parameters.AddWithValue("@vcId", vcId);
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

        public bool CapNhatThongTin(int id, string soLuong, string ghiChu)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE VatChatNguoiDung 
                                  SET SoLuong = @SoLuong, GhiChu = @GhiChu
                                  WHERE vcId = @Id AND UserId = @UserId";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@SoLuong", soLuong ?? "");
                        command.Parameters.AddWithValue("@GhiChu", ghiChu ?? "");
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
                    string sql = "SELECT * FROM VatChatNguoiDung WHERE UserId = @UserId";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
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