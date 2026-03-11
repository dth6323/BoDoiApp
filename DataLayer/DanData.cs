using BoDoiApp.Resources;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    public static class DanData
    {
        public static void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                var command = new SQLiteCommand(@"
                    CREATE TABLE IF NOT EXISTS Dan (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ItemCode TEXT NOT NULL,
    SK INTEGER NOT NULL,
    Tr1_1V REAL DEFAULT 0.0,
    SV_CS REAL DEFAULT 0,
    TL_1CS REAL DEFAULT 0.0,
    Note TEXT NOT NULL
);", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void InsertOrUpdateDan(string userId, string itemCode, double tr1_1V, double sv_cs, double tl_1cs, string sk, string note)
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                // Check if record exists
                var checkCommand = new SQLiteCommand(@"
                    SELECT COUNT(*) FROM Dan 
                    WHERE UserId = @UserId AND ItemCode = @ItemCode AND Note = @Note", connection);
                checkCommand.Parameters.AddWithValue("@UserId", userId);
                checkCommand.Parameters.AddWithValue("@ItemCode", itemCode);
                checkCommand.Parameters.AddWithValue("@Note", note);

                var exists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;

                SQLiteCommand command;
                if (exists)
                {
                    // Update existing record
                    command = new SQLiteCommand(@"
                        UPDATE Dan SET 
                            SK = @SK,
                            Tr1_1V = @Tr1_1V,
                            SV_CS = @SV_CS,
                            TL_1CS = @TL_1CS
                        WHERE UserId = @UserId AND ItemCode = @ItemCode AND Note = @Note", connection);
                }
                else
                {
                    // Insert new record
                    command = new SQLiteCommand(@"
                        INSERT INTO Dan (UserId, ItemCode, SK, Tr1_1V, SV_CS, TL_1CS, Note)
                        VALUES (@UserId, @ItemCode, @SK, @Tr1_1V, @SV_CS, @TL_1CS, @Note)", connection);
                }

                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@SK", sk);
                command.Parameters.AddWithValue("@Tr1_1V", tr1_1V);
                command.Parameters.AddWithValue("@SV_CS", sv_cs);
                command.Parameters.AddWithValue("@TL_1CS", tl_1cs);
                command.Parameters.AddWithValue("@Note", note);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateHangLoat(ReoGridControl ws, Dictionary<int, int> rows, string section)
        {
            foreach (var row in rows)
            {
                if (section == "Tiểu đoàn")
                {
                    var itemcode = ws.CurrentWorksheet.GetCellData(row.Value, 36)?.ToString() ?? string.Empty;
                    var sk = ws.CurrentWorksheet.GetCellData(row.Value, 37)?.ToString() ?? string.Empty;
                    var trl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 38) ?? 0.0);
                    var svcs = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 39) ?? 0.0);
                    var tl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 40) ?? 0.0);
                    var userId = Constants.CURRENT_USER_ID_VALUE;
                    InsertOrUpdateDan(userId, itemcode, trl, svcs, tl, sk, section);
                }
                else if(section == "Phối thuộc")
                {
                    var itemcode = ws.CurrentWorksheet.GetCellData(row.Value, 42)?.ToString() ?? string.Empty;
                    var sk = ws.CurrentWorksheet.GetCellData(row.Value, 43)?.ToString() ?? string.Empty;
                    var trl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 44) ?? 0.0);
                    var svcs = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 45) ?? 0.0);
                    var tl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 46) ?? 0.0);
                    var userId = Constants.CURRENT_USER_ID_VALUE;
                    InsertOrUpdateDan(userId, itemcode, trl, svcs, tl, sk, section);
                }
                else
                {
                    var itemcode = ws.CurrentWorksheet.GetCellData(row.Value, 30)?.ToString() ?? string.Empty;
                    var sk = ws.CurrentWorksheet.GetCellData(row.Value, 31)?.ToString() ?? string.Empty;
                    var trl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 32) ?? 0.0);
                    var svcs = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 33) ?? 0.0);
                    var tl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 34) ?? 0.0);
                    var userId = Constants.CURRENT_USER_ID_VALUE;
                    InsertOrUpdateDan(userId, itemcode, trl, svcs, tl, sk, section);
                }
            }
        }
        
        public static void LoadAllCell(ReoGridControl reoGridControl1, string Section)
        {
            //reoGridControl1.CurrentWorksheet.HideColumns(0, 30);
            //reoGridControl1.CurrentWorksheet.HideRows(0, 26);

            var ws = reoGridControl1.CurrentWorksheet;
            //reoGridControl1.SheetTabVisible = false;
            //ws.SetSettings(WorksheetSettings.View_ShowHeaders, false);
            LockCells(ws);

            switch (Section)
            {
                case "Toàn d":
                    //reoGridControl1.CurrentWorksheet.HideRows(41, 100);
                   Dictionary<int,int> rows = new Dictionary<int, int>() {
                         {0,28},
                         {1,30},
                         {2,31},
                         {3,32},
                         {4,33},
                         {5,35},
                         {6,36},
                         {7,37},
                         {8,38},
                         {9,39},
                         {10,40},
                    };

                    LoadData(reoGridControl1, rows, "Toàn d");
                    LoadData(reoGridControl1, rows, "Tiểu đoàn");
                    LoadData(reoGridControl1, rows, "Phối thuộc");
                    //ws.HideColumns(47, ws.ColumnCount - 47);
                    //ws.HideRows(41, ws.RowCount - 41);

                    break;
                case "Hướng chủ yếu":
                    //reoGridControl1.CurrentWorksheet.HideRows(26, 15);
                    //reoGridControl1.CurrentWorksheet.HideRows(56, 101);
                    rows = new Dictionary<int, int>() {
                             {0,43},
                             {1,45},
                             {2,46},
                             {3,47},
                             {4,48},
                             {5,50},
                             {6,51},
                             {7,52},
                             {8,53},
                             {9,54},
                             {10,55},
                    };
                    LoadData(reoGridControl1, rows, Section);

                    //ws.HideColumns(35, ws.ColumnCount - 35);
                    //ws.HideRows(56, ws.RowCount - 56);

                    break;
                case "Hướng thứ yếu":

                    //reoGridControl1.CurrentWorksheet.HideRows(26, 30);
                    //reoGridControl1.CurrentWorksheet.HideRows(71, 101);
                    rows = new Dictionary<int, int>() {
                         {0,58},
                         {1,60},
                         {2,61},
                         {3,62},
                         {4,63},
                         {5,64},
                         {6,66},
                         {7,67},
                         {8,68},
                         {9,69},
                         {10,70 },
                    };
                    LoadData(reoGridControl1, rows, Section);

                    //ws.HideColumns(35, ws.ColumnCount - 35);
                    //ws.HideRows(71, ws.RowCount - 71);
                    break;
                case "BP PNPS":
                    //reoGridControl1.CurrentWorksheet.HideRows(26, 45);
                    //reoGridControl1.CurrentWorksheet.HideRows(86, 101);
                    rows = new Dictionary<int, int>() {
                         {0,73},
                         {1,75},
                         {2,76},
                         {3,77},
                         {4,78},
                         {5,80},
                         {6,81},
                         {7,82},
                         {8,83},
                         {9,84},
                         {10,85 },
                    };
                    LoadData(reoGridControl1, rows, Section);
                    //ws.HideColumns(35, ws.ColumnCount - 35);
                    //ws.HideRows(86, ws.RowCount - 86);
                    break;
                case "LL còn lại":
                    //reoGridControl1.CurrentWorksheet.HideRows(0, 86);

                    rows = new Dictionary<int, int>() {
                         {0,88},
                         {1,90},
                         {2,91},
                         {3,92},
                         {4,93},
                         {5,95},
                         {6,96},
                         {7,97},
                         {8,98},
                         {9,99},
                         {10,100},
                    };
                    LoadData(reoGridControl1, rows, Section);
                    //ws.HideColumns(35, ws.ColumnCount - 35);
                    //ws.HideRows(101, ws.RowCount - 101);
                    break;
            }
        }
        
        private static void LoadData(ReoGridControl reoGridControl1, Dictionary<int, int> rows, string section)
        {
            string sql = $"SELECT ItemCode,Tr1_1V, SK, SV_CS, TL_1CS FROM Dan WHERE UserId = '{Constants.CURRENT_USER_ID_VALUE}' AND Note = '{section}'";

            int[] skipCols = { 31, 34, 37, 40, 43, 46 };

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    int index = 0;

                    if (section == "Tiểu đoàn")
                    {
                        while (reader.Read())
                        {
                            if (index >= rows.Count)
                                break;
                                
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();
                            var sk = reader["SK"].ToString();
                             var tl = reader["TL_1CS"].ToString();
                            if (!skipCols.Contains(35))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 36, item);
                            reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 37, sk);

                            if (!skipCols.Contains(37))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 38, trl);

                            if (!skipCols.Contains(38))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 39, svcs);
                            index++;
                        }
                    }
                    else if (section == "Phối thuộc")
                    {
                        while (reader.Read())
                        {
                            if (index >= rows.Count)
                                break;
                                
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();
                            var sk = reader["SK"].ToString();

                            if (!skipCols.Contains(41))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 42, item);
                            reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 43, sk);
                            if (!skipCols.Contains(43))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 44, trl);

                            if (!skipCols.Contains(44))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 45, svcs);

                            index++;
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (index >= rows.Count)
                                break;
                                
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();
                            var sk = reader["SK"].ToString();

                            if (!skipCols.Contains(30))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 30, item);
                            reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 31, sk);
                            if (!skipCols.Contains(32))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 32, trl);

                            if (!skipCols.Contains(33))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 33, svcs);

                            index++;
                        }
                    }
                }
            }
        }
        
        private static void LockCells(Worksheet ws)
        {
            if (ws == null) return;

            // Lock all cells first
            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    ws.Cells[r, c].IsReadOnly = true;
                }
            }

            // Unlock specific cells for data entry (adjust based on your requirements)
            // This is a generic implementation - you may need to customize based on your specific needs
            int[] editableColumns = { 30, 32, 33, 36, 38, 39, 42, 44, 45 };
            
            for (int r = 28; r < ws.RowCount && r <= 100; r++)
            {
                foreach (var col in editableColumns)
                {
                    if (col < ws.ColumnCount)
                    {
                        ws.Cells[r, col].IsReadOnly = false;
                    }
                }
            }
        }
    }
}
