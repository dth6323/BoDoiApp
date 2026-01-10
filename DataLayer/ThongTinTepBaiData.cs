using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class ThongTinTapBaiData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public ThongTinTapBaiData()
        {
        }

        // ================================================================
        //  THÊM THÔNG TIN
        // ================================================================
        public bool ThemThongTin(
            string thongtintapbai,
            string vitrichihuy,
            string thoigian,
            string manh1,
            string manh2,
            string manh3,
            string manh4,
            string tyle,
            string nam,
            string chihuy_hckt,
            string nguoithaythe
        )
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO thongtintapbai 
                                (thongtintapbai, vitrichihuy, thoigian,
                                 manh1, manh2, manh3, manh4,
                                 tyle, nam, chihuy_hckt, nguoithaythe, user)
                                VALUES
                                (@thongtintapbai, @vitrichihuy, @thoigian,
                                 @manh1, @manh2, @manh3, @manh4,
                                 @tyle, @nam, @chihuy_hckt, @nguoithaythe, @user)";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@thongtintapbai", thongtintapbai ?? "");
                        cmd.Parameters.AddWithValue("@vitrichihuy", vitrichihuy ?? "");
                        cmd.Parameters.AddWithValue("@thoigian", thoigian ?? "");
                        cmd.Parameters.AddWithValue("@manh1", manh1 ?? "");
                        cmd.Parameters.AddWithValue("@manh2", manh2 ?? "");
                        cmd.Parameters.AddWithValue("@manh3", manh3 ?? "");
                        cmd.Parameters.AddWithValue("@manh4", manh4 ?? "");
                        cmd.Parameters.AddWithValue("@tyle", tyle ?? "");
                        cmd.Parameters.AddWithValue("@nam", nam ?? "");
                        cmd.Parameters.AddWithValue("@chihuy_hckt", chihuy_hckt ?? "");
                        cmd.Parameters.AddWithValue("@nguoithaythe", nguoithaythe ?? "");
                        cmd.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi thêm thông tin: {ex.Message}");
                return false;
            }
        }

        // ================================================================
        //  CẬP NHẬT THÔNG TIN THEO USER
        // ================================================================
        public bool CapNhatThongTin(
            string thongtintapbai,
            string vitrichihuy,
            string thoigian,
            string manh1,
            string manh2,
            string manh3,
            string manh4,
            string tyle,
            string nam,
            string chihuy_hckt,
            string nguoithaythe
        )
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE thongtintapbai SET
                                      thongtintapbai = @thongtintapbai,
                                      vitrichihuy = @vitrichihuy,
                                      thoigian = @thoigian,
                                      manh1 = @manh1,
                                      manh2 = @manh2,
                                      manh3 = @manh3,
                                      manh4 = @manh4,
                                      tyle = @tyle,
                                      nam = @nam,
                                      chihuy_hckt = @chihuy_hckt,
                                      nguoithaythe = @nguoithaythe
                                   WHERE user = @user";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@thongtintapbai", thongtintapbai ?? "");
                        cmd.Parameters.AddWithValue("@vitrichihuy", vitrichihuy ?? "");
                        cmd.Parameters.AddWithValue("@thoigian", thoigian ?? "");
                        cmd.Parameters.AddWithValue("@manh1", manh1 ?? "");
                        cmd.Parameters.AddWithValue("@manh2", manh2 ?? "");
                        cmd.Parameters.AddWithValue("@manh3", manh3 ?? "");
                        cmd.Parameters.AddWithValue("@manh4", manh4 ?? "");
                        cmd.Parameters.AddWithValue("@tyle", tyle ?? "");
                        cmd.Parameters.AddWithValue("@nam", nam ?? "");
                        cmd.Parameters.AddWithValue("@chihuy_hckt", chihuy_hckt ?? "");
                        cmd.Parameters.AddWithValue("@nguoithaythe", nguoithaythe ?? "");
                        cmd.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}");
                return false;
            }
        }

        // ================================================================
        //  LẤY THÔNG TIN THEO USER
        // ================================================================
        public DataTable LayThongTin()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM thongtintapbai WHERE user = @user";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi lấy thông tin: {ex.Message}");
                return null;
            }

            return dt;
        }
    }
}
