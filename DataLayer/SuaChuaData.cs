using System;
using System.Data.SQLite;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    public static class SuaChuaData
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
                        // Xóa dữ liệu cũ theo user
                        string deleteSql = "DELETE FROM suachua_tbkt WHERE User=@User";
                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO suachua_tbkt
                        (sl, ty_le_hu_hong, nhe, vua, nang, huy, cong, User)
                        VALUES
                        (@sl, @ty_le, @nhe, @vua, @nang, @huy, @cong, @User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sl", System.Data.DbType.Double);
                            // Tạo parameter trước
                            cmd.Parameters.Add("@ty_le", System.Data.DbType.Double);
                            cmd.Parameters.Add("@nhe", System.Data.DbType.Double);
                            cmd.Parameters.Add("@vua", System.Data.DbType.Double);
                            cmd.Parameters.Add("@nang", System.Data.DbType.Double);
                            cmd.Parameters.Add("@huy", System.Data.DbType.Double);
                            cmd.Parameters.Add("@cong", System.Data.DbType.Double);
                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            int row = 3; // dòng đầu dữ liệu (theo ảnh của bạn)

                            while (true)
                            {
                                var loai = ws.GetCellData(row, 1)?.ToString();
                                var sl = ws.GetCellData(row, 1);
                                var tyle = ws.GetCellData(row, 2);
                                var nhe = ws.GetCellData(row, 3);
                                var vua = ws.GetCellData(row, 4);
                                var nang = ws.GetCellData(row, 5);
                                var huy = ws.GetCellData(row, 6);
                                var cong = ws.GetCellData(row, 7);

                                // Nếu cả dòng trống thì dừng
                                if (sl == null && tyle == null && nhe == null && vua == null &&
                                    nang == null && huy == null && cong == null)
                                    break;
                                cmd.Parameters["@sl"].Value = GetDouble(ws.GetCellData(row, 1));
                                cmd.Parameters["@ty_le"].Value = GetDouble(ws.GetCellData(row, 2));
                                cmd.Parameters["@nhe"].Value = GetDouble(ws.GetCellData(row, 3));
                                cmd.Parameters["@vua"].Value = GetDouble(ws.GetCellData(row, 4));
                                cmd.Parameters["@nang"].Value = GetDouble(ws.GetCellData(row, 5));
                                cmd.Parameters["@huy"].Value = GetDouble(ws.GetCellData(row, 6));
                                cmd.Parameters["@cong"].Value = GetDouble(ws.GetCellData(row, 7));
                                cmd.Parameters["@User"].Value = Properties.Settings.Default.Username;
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
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM suachua_tbkt WHERE User=@User";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 3;

                        while (reader.Read())
                        {
                            ws.SetCellData(row, 1, reader["sl"]);
                            ws.SetCellData(row, 2, reader["ty_le_hu_hong"]);
                            ws.SetCellData(row, 3, reader["nhe"]);
                            ws.SetCellData(row, 4, reader["vua"]);
                            ws.SetCellData(row, 5, reader["nang"]);
                            ws.SetCellData(row, 6, reader["huy"]);
                            ws.SetCellData(row, 7, reader["cong"]);
                            row++;
                        }
                    }
                }
            }

            grid.Refresh();
        }
    }
}