using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    Tr1_1V REAL DEFAULT 0.0,
    SV_CS REAL DEFAULT 0,
    TL_1CS REAL DEFAULT 0.0,
    Note TEXT,
);", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
