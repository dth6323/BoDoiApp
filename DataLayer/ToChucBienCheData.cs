using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class ToChucBienCheData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public bool Them(
            string tieudoan,
            string tieudoan_qs_tbkt,
            string huong_chu_yeu,
            string phong_ngu_phia_sau,
            string bo_phan,
            string bo_phan_qs_tbkt,
            string luc_luong_con_lai,
            string loai_huong
        )
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO tochucbienche 
                        (tieudoan, tieudoan_qs_tbkt, huong_chu_yeu, phong_ngu_phia_sau, 
                         bo_phan, bo_phan_qs_tbkt, luc_luong_con_lai, loai_huong, user)
                        VALUES 
                        (@tieudoan, @qs1, @huongchuyeu, @phongngu, @bophan, @qs2, @lucluong, @loai, @user)";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@tieudoan", tieudoan);
                        cmd.Parameters.AddWithValue("@qs1", tieudoan_qs_tbkt);
                        cmd.Parameters.AddWithValue("@huongchuyeu", huong_chu_yeu);
                        cmd.Parameters.AddWithValue("@phongngu", phong_ngu_phia_sau);
                        cmd.Parameters.AddWithValue("@bophan", bo_phan);
                        cmd.Parameters.AddWithValue("@qs2", bo_phan_qs_tbkt);
                        cmd.Parameters.AddWithValue("@lucluong", luc_luong_con_lai);
                        cmd.Parameters.AddWithValue("@loai", loai_huong);
                        cmd.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi lưu tổ chức biên chế: {ex.Message}");
                return false;
            }
        }

        public DataTable LayTheoUser()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM tochucbienche WHERE user = @u";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@u", Properties.Settings.Default.Username);
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return dt;
        }
    }
}
