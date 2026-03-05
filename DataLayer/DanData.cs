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
                var command = new SQLiteCommand(@"
                    INSERT INTO Dan (UserId, ItemCode, SK, Tr1_1V, SV_CS, TL_1CS,Note)
                    VALUES (@UserId, @ItemCode, @SK, @Tr1_1V, @SV_CS, @TL_1CS, @Note)
                    ON CONFLICT(UserId, ItemCode,Note) DO UPDATE SET
                        SK = excluded.SK,
                        Tr1_1V = excluded.Tr1_1V,
                        SV_CS = excluded.SV_CS,
                        TL_1CS = excluded.TL_1CS;", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@SK", sk);
                command.Parameters.AddWithValue("@Note", note);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@Tr1_1V", tr1_1V);
                command.Parameters.AddWithValue("@SV_CS", sv_cs);
                command.Parameters.AddWithValue("@TL_1CS", tl_1cs);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateHangLoat(ReoGridControl ws, Dictionary<int, int> rows, string section)
        {
            foreach (var row in rows)
            {
                var itemcode = ws.CurrentWorksheet.GetCellData(row.Value, 0)?.ToString() ?? string.Empty;
                var sk = ws.CurrentWorksheet.GetCellData(row.Value, 1)?.ToString() ?? string.Empty;
                var trl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 2) ?? 0.0);
                var svcs = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 3) ?? 0.0);
                var tl = Convert.ToDouble(ws.CurrentWorksheet.GetCellData(row.Value, 4) ?? 0.0);
                var userId = Constants.CURRENT_USER_ID_VALUE;
                InsertOrUpdateDan(userId, itemcode, trl, svcs, tl, sk, section);
            }
        }
    }
}
