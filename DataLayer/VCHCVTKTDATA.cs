using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    internal class VCHCVTKTDATA
    {
        private const string connectionString = "Data Source=data.db;Version=3;";
        private static readonly HashSet<int> SkipRows = new HashSet<int>
{
    1,2,3,4,5,11,16
};
        // Convert Excel row -> ReoGrid row
        static int R(int excelRow) => excelRow - 1;

        // ===== ROW DEFINITIONS =====

        static int[] rows_O = { R(9), R(10), R(11), R(12), R(13), R(15), R(16), R(17), R(18), R(20), R(21) };

        static int[] rows_FGHJN =
        {
            R(30),R(31),R(32),R(36),
            R(45),R(46),R(47),
            R(51),
            R(60),R(61),R(62),
            R(66)
        };

        static int[] rows_P = { R(45), R(46), R(47) };

        static int[] rows_U = { R(45), R(46), R(47), R(60), R(61), R(62) };

        static int[] rows_Zblock = CreateRange(R(7), R(81));

        static int[] CreateRange(int start, int end)
        {
            List<int> list = new List<int>();
            for (int i = start; i <= end; i++)
                list.Add(i);
            return list.ToArray();
        }

        static double GetDouble(object v)
        {
            if (v == null) return 0;
            if (double.TryParse(v.ToString(), out double d)) return d;
            return 0;
        }
        public static void LoadAllVatChat(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT *
                       FROM VatChat
                       WHERE UserId=@User
                       ORDER BY TT";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;

                        int row = 6;

                        while (reader.Read() && row <= 17) // đến I18
                        {
                            ws.SetCellData(row, 3, reader["PC_TDQ_KhoD"]);
                            ws.SetCellData(row, 4, reader["PC_TDQ_DonVi"]);
                            ws.SetCellData(row, 5, reader["PC_TDQ_Plus"]);

                            ws.SetCellData(row, 6, reader["PC_SCD_KhoD"]);
                            ws.SetCellData(row, 7, reader["PC_SCD_DonVi"]);
                            ws.SetCellData(row, 8, reader["PC_SCD_Plus"]);

                            row++;
                        }
                    }
                }
            }
        }
        // ================= SAVE =================
        public static void LoadAllPhanCap(ReoGridControl grid, int skip = 1)
        {
            var userName = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userName))
                return;

            var sheet = grid.CurrentWorksheet;
            if (sheet == null)
                throw new InvalidOperationException("Current worksheet is not available.");

            const int COL_TT = 0;
            const int COL_LOAIVATCHAT = 1;
            const int COL_DVT = 2;

            const int COL_PC_TDQ_KHOD = 3;
            const int COL_PC_TDQ_DONVI = 4;
            const int COL_PC_TDQ_PLUS = 5;

            const int COL_PC_SCD_KHOD = 6;
            const int COL_PC_SCD_DONVI = 7;
            const int COL_PC_SCD_PLUS = 8;

            const int startRow = 5;

            try
            {
                var dt = new DataTable();

                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = connection.CreateCommand())
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    connection.Open();

                    command.CommandText = @"
SELECT
    vc.TT,
    vc.LoaiVatChat,
    vc.DVT,
    vc.PC_TDQ_KhoD,
    vc.PC_TDQ_DonVi,
    vc.PC_TDQ_Plus,
    vc.PC_SCD_KhoD,
    vc.PC_SCD_DonVi,
    vc.PC_SCD_Plus
FROM VatChat vc
WHERE vc.UserId = @UserName
ORDER BY vc.TT ASC;";

                    command.Parameters.AddWithValue("@UserName", userName.Trim());
                    adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                    return;

                int requiredRows = startRow + dt.Rows.Count;
                if (sheet.RowCount < requiredRows)
                    sheet.AppendRows(requiredRows - sheet.RowCount);

                int rowIndex = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = startRow + rowIndex;

                        while (SkipRows.Contains(r + 1))
                        {
                            rowIndex++;
                            r = startRow + rowIndex;
                        }
                    

                    var row = dt.Rows[i];

                    sheet[r, COL_TT] = row["TT"] == DBNull.Value ? null : row["TT"];
                    sheet[r, COL_LOAIVATCHAT] = row["LoaiVatChat"] == DBNull.Value ? "" : Convert.ToString(row["LoaiVatChat"]);
                    sheet[r, COL_DVT] = row["DVT"] == DBNull.Value ? "" : Convert.ToString(row["DVT"]);

                    sheet[r, COL_PC_TDQ_KHOD] = row["PC_TDQ_KhoD"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_KhoD"]);
                    sheet[r, COL_PC_TDQ_DONVI] = row["PC_TDQ_DonVi"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_DonVi"]);
                    sheet[r, COL_PC_TDQ_PLUS] = row["PC_TDQ_Plus"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_Plus"]);

                    sheet[r, COL_PC_SCD_KHOD] = row["PC_SCD_KhoD"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_KhoD"]);
                    sheet[r, COL_PC_SCD_DONVI] = row["PC_SCD_DonVi"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_DonVi"]);
                    sheet[r, COL_PC_SCD_PLUS] = row["PC_SCD_Plus"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_Plus"]);

                    rowIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load data failed: " + ex.Message);
            }
        }

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
                        string delete = "DELETE FROM VCHCVTKT WHERE UserId=@User";

                        using (var cmd = new SQLiteCommand(delete, con, tran))
                        {
                            cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            cmd.ExecuteNonQuery();
                        }

                        string insert = @"INSERT INTO VCHCVTKT (Row,Col,Value,UserId)
                                  VALUES (@Row,@Col,@Value,@User)";

                        using (var cmd = new SQLiteCommand(insert, con, tran))
                        {
                            var pRow = cmd.Parameters.Add("@Row", DbType.Int32);
                            var pCol = cmd.Parameters.Add("@Col", DbType.Int32);
                            var pValue = cmd.Parameters.Add("@Value", DbType.Double);
                            var pUser = cmd.Parameters.Add("@User", DbType.String);

                            pUser.Value = Properties.Settings.Default.Username;

                            for (int r = 6; r <= 80; r++)      // Row 7 → 81
                            {
                                for (int c = 2; c <= 28; c++)  // C → AC
                                {
                                    pRow.Value = r;
                                    pCol.Value = c;
                                    pValue.Value = GetDouble(ws.GetCellData(r, c));

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
        static bool IsCellAllowed(int r, int c)
        {
            if (c == 14 && Array.Exists(rows_O, x => x == r)) return true;

            if ((c == 5 || c == 6 || c == 7 || c == 9 || c == 13 || c == 17) &&
                Array.Exists(rows_FGHJN, x => x == r)) return true;

            if (c == 15 && Array.Exists(rows_P, x => x == r)) return true;

            if (c == 20 && Array.Exists(rows_U, x => x == r)) return true;

            if ((c == 25 || c == 26 || c == 27 || c == 28) &&
                Array.Exists(rows_Zblock, x => x == r)) return true;

            return false;
        }
        // ================= LOAD =================
        public static void LoadTrangKiThuat(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string sql = @"SELECT * FROM trangkithuat WHERE User = @User";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string ll = rd["ll"]?.ToString()?.Trim();
                            string option = rd["option"]?.ToString()?.Trim();

                            void LoadRow(int r)
                            {
                                var cellText = ws.GetCellData(R(r), 0)?.ToString()?.Trim();
                                if (cellText != ll) return;

                                int row = R(r);

                                ws.SetCellData(row, 1, rd["quan_so"]);
                                ws.SetCellData(row, 2, rd["sn"]);
                                ws.SetCellData(row, 3, rd["tl"]);
                                ws.SetCellData(row, 4, rd["trl"]);
                                ws.SetCellData(row, 5, rd["dl"]);
                                ws.SetCellData(row, 6, rd["b41_m79"]);
                                ws.SetCellData(row, 7, rd["luu_dan"]);
                                ws.SetCellData(row, 8, rd["coi_60"]);
                                ws.SetCellData(row, 9, rd["coi_82"]);
                                ws.SetCellData(row, 10, rd["coi_100"]);
                                ws.SetCellData(row, 11, rd["pct_spg9"]);
                                ws.SetCellData(row, 12, rd["phao_pk_127"]);
                                ws.SetCellData(row, 13, rd["ons"]);
                                ws.SetCellData(row, 14, rd["db"]);
                            }

                            if (option == "Tieu Doan")
                            {
                                LoadRow(6);
                                LoadRow(7);
                                LoadRow(8);
                                LoadRow(19);
                            }

                            if (option == "Hướng Chủ yếu")
                            {
                                LoadRow(25);
                                LoadRow(26);
                                LoadRow(27);
                                LoadRow(36);
                            }

                            if (option == "Hướng Thứ Yếu")
                            {
                                LoadRow(42);
                                LoadRow(43);
                                LoadRow(44);
                                LoadRow(51);
                            }

                            if (option == "Phòng ngự phía sau")
                            {
                                LoadRow(57);
                                LoadRow(58);
                                LoadRow(59);
                                LoadRow(62);
                            }

                            if (option == "LL còn lại")
                            {
                                LoadRow(68);
                                LoadRow(69);
                                LoadRow(70);
                                LoadRow(81);
                            }
                        }
                    }
                }
            }
        }
        public static void LockSheet(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;
            if (ws == null) return;

            // 1. Lock toàn bộ sheet
            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    ws.Cells[r, c].IsReadOnly = true;
                }
            }

            // 2. Mở khóa các ô được phép nhập
            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    if (IsCellAllowed(r, c))
                    {
                        ws.Cells[r, c].IsReadOnly = false;
                    }
                }
            }
        }
        public static void LoadAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string sql = @"SELECT Row,Col,Value
                       FROM VCHCVTKT
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

                            if (IsCellAllowed(r, c))
                                ws.SetCellData(r, c, rd["Value"]);
                        }
                    }
                }
            }
        }
    }
}