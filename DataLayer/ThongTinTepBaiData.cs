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

        public bool ThemThongTin(string tendaubai, string sochuy, string bandotapbai,string manh1, string manh2, string manh3, string manh4, string chihuyduan,
            string chihuyhaucan, string chihuyduan_tt, string chihuytieudoan_tt,
            string captren, string capminh)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO thongtintapbai 
                (tenvankien, vitrichihuy, thoigian, 
                 manh1, manh2, manh3, manh4,
                 tyle, nam, chihuy_hckt, nguoithaythe, user)
                VALUES 
                (@tenvankien, @vitrichihuy, @thoigian,
                 @manh1, @manh2, @manh3, @manh4,
                 @tyle, @nam, @chihuy_hckt, @nguoithaythe, @user)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tenvankien", tenvankien);
                        command.Parameters.AddWithValue("@vitrichihuy", vitrichihuy);
                        command.Parameters.AddWithValue("@thoigian", thoigian);

                        command.Parameters.AddWithValue("@manh1", manh1);
                        command.Parameters.AddWithValue("@manh2", manh2);
                        command.Parameters.AddWithValue("@manh3", manh3);
                        command.Parameters.AddWithValue("@manh4", manh4);

                        command.Parameters.AddWithValue("@tyle", tyle);
                        command.Parameters.AddWithValue("@nam", nam);
                        command.Parameters.AddWithValue("@chihuy_hckt", chihuy_hckt);
                        command.Parameters.AddWithValue("@nguoithaythe", nguoithaythe);

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


        public bool CapNhatThongTin(
    int id,
    string tenvankien,
    string vitrichihuy,
    string thoigian,
    string manh1,
    string manh2,
    string manh3,
    string manh4,
    string tyle,
    string nam,
    string chihuy_hckt,
    string nguoithaythe)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE thongtintapbai SET
                tenvankien = @tenvankien,
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
                WHERE id = @id";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@tenvankien", tenvankien);
                        command.Parameters.AddWithValue("@vitrichihuy", vitrichihuy);
                        command.Parameters.AddWithValue("@thoigian", thoigian);

                        command.Parameters.AddWithValue("@manh1", manh1);
                        command.Parameters.AddWithValue("@manh2", manh2);
                        command.Parameters.AddWithValue("@manh3", manh3);
                        command.Parameters.AddWithValue("@manh4", manh4);

                        command.Parameters.AddWithValue("@tyle", tyle);
                        command.Parameters.AddWithValue("@nam", nam);
                        command.Parameters.AddWithValue("@chihuy_hckt", chihuy_hckt);
                        command.Parameters.AddWithValue("@nguoithaythe", nguoithaythe);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Lỗi cập nhật thông tin: {ex.Message}");
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

                    string sql = "SELECT * FROM thongtintapbai WHERE user = @user";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@user", Properties.Settings.Default.Username);

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Lỗi lấy thông tin: {ex.Message}");
                return null;
            }

            return dt;
        }

    }