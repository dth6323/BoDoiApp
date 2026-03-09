using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    internal class KeHoachSuaChuaData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";
        private static double GetDouble(object value)
        {
            if (value == null) return 0;

            if (double.TryParse(value.ToString(), out double result))
                return result;

            return 0;
        }
        public static void SaveAll(ReoGridControl grid)
        {
            grid.Focus();
            Application.DoEvents();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Xóa dữ liệu cũ theo User
                        string deleteSql = "DELETE FROM kehoachsuachua WHERE User=@User";
                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"
                INSERT INTO kehoachsuachua (
                    loai_tbkt, so_luong, ty_le_hu_hong,
                    tong_nhe, tong_vua, tong_nang, tong_huy, tong_cong,
                    kha_nhe, kha_vua, kha_cong,
                    con_nhe, con_vua, con_nang, con_huy, con_cong,
                    User
                )
                VALUES (
                    @loai, @soluong, @tyle,
                    @tong_nhe, @tong_vua, @tong_nang, @tong_huy, @tong_cong,
                    @kha_nhe, @kha_vua, @kha_cong,
                    @con_nhe, @con_vua, @con_nang, @con_huy, @con_cong,
                    @User
                )";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            int row = 2; // dòng bắt đầu dữ liệu thực tế (theo ảnh)

                            while (true)
                            {
                                var loai = ws.GetCellData(row, 1)?.ToString();
                                if (string.IsNullOrWhiteSpace(loai))
                                    break;

                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@loai", loai);
                                cmd.Parameters.AddWithValue("@soluong", GetDouble(ws.GetCellData(row, 2)));
                                cmd.Parameters.AddWithValue("@tyle", GetDouble(ws.GetCellData(row, 3)));

                                // Tổng số hư hỏng
                                cmd.Parameters.AddWithValue("@tong_nhe", GetDouble(ws.GetCellData(row, 4)));
                                cmd.Parameters.AddWithValue("@tong_vua", GetDouble(ws.GetCellData(row, 5)));
                                cmd.Parameters.AddWithValue("@tong_nang", GetDouble(ws.GetCellData(row, 6)));
                                cmd.Parameters.AddWithValue("@tong_huy", GetDouble(ws.GetCellData(row, 7)));
                                cmd.Parameters.AddWithValue("@tong_cong", GetDouble(ws.GetCellData(row, 8)));

                                // Khả năng sửa chữa
                                cmd.Parameters.AddWithValue("@kha_nhe", GetDouble(ws.GetCellData(row, 9)));
                                cmd.Parameters.AddWithValue("@kha_vua", GetDouble(ws.GetCellData(row, 10)));
                                cmd.Parameters.AddWithValue("@kha_cong", GetDouble(ws.GetCellData(row, 11)));

                                // Còn lại
                                cmd.Parameters.AddWithValue("@con_nhe", GetDouble(ws.GetCellData(row, 12)));
                                cmd.Parameters.AddWithValue("@con_vua", GetDouble(ws.GetCellData(row, 13)));
                                cmd.Parameters.AddWithValue("@con_nang", GetDouble(ws.GetCellData(row, 14)));
                                cmd.Parameters.AddWithValue("@con_huy", GetDouble(ws.GetCellData(row, 15)));
                                cmd.Parameters.AddWithValue("@con_cong", GetDouble(ws.GetCellData(row, 16)));

                                cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                                cmd.ExecuteNonQuery();
                                row++;
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Lưu thành công!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi lưu: " + ex.Message);
                    }
                }
            }
        }
        public static void LoadAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
        SELECT 
            ty_le_hu_hong,      -- D
            kha_nhe,       -- J
            kha_vua        -- K
        FROM kehoachsuachua
        WHERE User=@User
        LIMIT 10";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int row = 2;

                        while (reader.Read() && row <= 11)
                        {
                            ws.SetCellData(row, 3, reader["ty_le_hu_hong"]); // D
                            ws.SetCellData(row, 9, reader["kha_nhe"]);  // J
                            ws.SetCellData(row, 10, reader["kha_vua"]); // K
                            row++;
                        }
                    }
                }
            }

            grid.Refresh();
        }
    }
}
