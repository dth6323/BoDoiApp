using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class ThongTinTapBai
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public ThongTinTapBai()
        {
        }

        public bool ThemThongTin(string tendaubai, string sochuy, string bandotapbai,string manh1, string manh2, string manh3, string manh4, string chihuytieudoan,
            string chihuyhaucan, string chihuyduan_tt, string chihuytieudoan_tt, string phienhedonvitapbai,
            string captren, string capminh)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO thongtintepbai (tendaubai, sochuy, bandotapbai,manh1,manh2,manh3,manh4,chihuytieudoan, chihuyhaucan, " +
                                 "chihuytieudoan_tt, chihuyhaucan_tt, captren, capminh,User) " +
                                 "VALUES (@tendaubai, @sochuy, @bandotapbai,@manh1,@manh2,@manh3,@manh4, @chihuytieudoan, @chihuyhaucan, @chihuytieudoan_tt, " +
                                 "@chihuyhaucan_tt, @captren, @capminh, @Id)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tendaubai", tendaubai);
                        command.Parameters.AddWithValue("@sochuy", sochuy);
                        command.Parameters.AddWithValue("@bandotapbai", bandotapbai);
                        command.Parameters.AddWithValue("@manh1", bandotapbai);
                        command.Parameters.AddWithValue("@manh2", manh2);
                        command.Parameters.AddWithValue("@manh3", manh3);
                        command.Parameters.AddWithValue("@manh4", manh4);
                        command.Parameters.AddWithValue("@chihuyduan", chihuytieudoan);
                        command.Parameters.AddWithValue("@chihuyhaucan", chihuyhaucan);
                        command.Parameters.AddWithValue("@chihuyduan_tt", chihuyduan_tt);
                        command.Parameters.AddWithValue("@chihuyhaucan_tt", chihuytieudoan_tt);
                        command.Parameters.AddWithValue("@captren", captren);
                        command.Parameters.AddWithValue("@capminh", capminh);
                        command.Parameters.AddWithValue("@Id", Properties.Settings.Default.Username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm thông tin!");
                return false;
            }
        }

        public bool CapNhatThongTin(int id, string tendaubai, string sochuy, string bandotapbai, string manh1, string manh2, string manh3, string manh4, string chihuytieudoan, string chihuyhaucan, string chihuytieudoan_tt, string chihuyhaucan_tt, string captren, string capminh)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE thongtintepbai 
                          SET tendaubai = @tendaubai,
                              sochuy = @sochuy,
                              bandotapbai = @bandotapbai,
                              manh1 = @manh1,
                              manh2 = @manh2,
                              manh3 = @manh3,
                              manh4 = @manh4,
                              chihuytieudoan = @chihuytieudoan,
                              chihuyhaucan = @chihuyhaucan,
                              chihuytieudoan_tt = @chihuytieudoan_tt,
                              chihuyhaucan_tt = @chihuyhaucan_tt,
                              captren = @captren,
                              capminh = @capminh
                          WHERE id = @id;";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@tendaubai", tendaubai);
                        command.Parameters.AddWithValue("@sochuy", sochuy);
                        command.Parameters.AddWithValue("@bandotapbai", bandotapbai);
                        command.Parameters.AddWithValue("@manh1", manh1);
                        command.Parameters.AddWithValue("@manh2", manh2);
                        command.Parameters.AddWithValue("@manh3", manh3);
                        command.Parameters.AddWithValue("@manh4", manh4);
                        command.Parameters.AddWithValue("@chihuytieudoan", chihuytieudoan);
                        command.Parameters.AddWithValue("@chihuyhaucan", chihuyhaucan);
                        command.Parameters.AddWithValue("@chihuytieudoan_tt", chihuytieudoan_tt);
                        command.Parameters.AddWithValue("@chihuyhaucan_tt", chihuyhaucan_tt);
                        command.Parameters.AddWithValue("@captren", captren);
                        command.Parameters.AddWithValue("@capminh", capminh);

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

        public bool LayThongTin()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM thongtintepbai WHERE User = @sochuy";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@sochuy", Properties.Settings.Default.Username);
                        using (var reader = command.ExecuteReader())
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lấy thông tin!");
                return false;
            }
        }
    }
}