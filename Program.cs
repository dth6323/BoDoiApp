using BoDoiApp.form;
using BoDoiApp.View;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp
{
    internal static class Program
    {

        private const string connectionString = "Data Source=data.db;Version=3;";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMana.Init();
            Application.Run(FormMana.Dangnhap);
        }

        private static void InitializeDatabase()
        {
            if (!File.Exists("schema.sql"))
            {
                throw new FileNotFoundException("Không tìm thấy file schema.sql");
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "CREATE TABLE IF NOT EXISTS Users (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    Username TEXT NOT NULL UNIQUE,\r\n    Password TEXT NOT NULL,\r\n    FullName TEXT\r\n);";
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                string sql1 = "CREATE TABLE IF NOT EXISTS thongtintepbai (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    tendaubai TEXT,\r\n    sochuy TEXT,\r\n    bandotapbai TEXT,\r\n    manh1 TEXT,\r\n    manh2 TEXT,\r\n    manh3 TEXT,\r\n    manh4 TEXT,\r\n    chihuyduan TEXT,\r\n    chihuyhaucan TEXT,\r\n    chihuyduan_tt TEXT,\r\n    chihuyhaucan_tt TEXT,\r\n    captren TEXT,\r\n    capminh TEXT,\r\n    User TEXT\r\n);";
                var command1 = new SQLiteCommand(sql1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
