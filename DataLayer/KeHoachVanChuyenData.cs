using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    internal class KeHoachVanChuyenData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";
        static readonly int[] SkipRows = { 6, 17, 28, 39, 50 };

        static bool IsLoadCell(int r, int c)
        {
            // bỏ qua các dòng
            if (SkipRows.Contains(r)) return false;

            // D18 -> D61
            if (c == 3 && r >= 17 && r <= 60) return true;

            // E7 -> M61
            if (c >= 4 && c <= 12 && r >= 6 && r <= 60) return true;

            return false;
        }

        static object GetValue(object v)
        {
            if (v == null) return DBNull.Value;

            var text = v.ToString().Trim();
            if (string.IsNullOrWhiteSpace(text))
                return DBNull.Value;

            return text;
        }

        // ================= SAVE =================
        public static void SaveAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string delete = "DELETE FROM KeHoachVanChuyen WHERE UserId=@User";

                        using (var cmd = new SQLiteCommand(delete, con, tran))
                        {
                            cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            cmd.ExecuteNonQuery();
                        }

                        string insert = @"INSERT INTO KeHoachVanChuyen (Row,Col,Value,UserId)
                                          VALUES (@Row,@Col,@Value,@User)";

                        using (var cmd = new SQLiteCommand(insert, con, tran))
                        {
                            var pRow = cmd.Parameters.Add("@Row", DbType.Int32);
                            var pCol = cmd.Parameters.Add("@Col", DbType.Int32);
                            var pValue = cmd.Parameters.Add("@Value", DbType.Object);
                            var pUser = cmd.Parameters.Add("@User", DbType.String);

                            pUser.Value = Properties.Settings.Default.Username;

                            // B7 -> M61
                            for (int r = 6; r <= 60; r++)
                            {
                                for (int c = 1; c <= 12; c++)
                                {
                                    pRow.Value = r;
                                    pCol.Value = c;
                                    pValue.Value = GetValue(ws.GetCellData(r, c));

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Lưu thành công");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public static void LockSheet(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;
            if (ws == null) return;

            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    if (IsLoadCell(r, c))
                        ws.Cells[r, c].IsReadOnly = false;
                    else
                        ws.Cells[r, c].IsReadOnly = true;
                }
            }
        }
        // ================= LOAD =================
        public static void LoadAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string sql = @"SELECT Row,Col,Value
                               FROM KeHoachVanChuyen
                               WHERE UserId=@User";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int r = Convert.ToInt32(rd["Row"]);
                            int c = Convert.ToInt32(rd["Col"]);

                            if (IsLoadCell(r, c))
                            {
                                ws.SetCellData(r, c, rd["Value"]);
                            }
                        }
                    }
                }
            }
        }
    }
}
