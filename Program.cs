using BoDoiApp.form;
using BoDoiApp.View;
using BoDoiApp.View.KhaiBaoDuLieuView;
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
            //if (!File.Exists("schema.sql"))
            //{
            //    throw new FileNotFoundException("Không tìm thấy file schema.sql");
            //}

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "CREATE TABLE IF NOT EXISTS Users (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    Username TEXT NOT NULL UNIQUE,\r\n    Password TEXT NOT NULL,\r\n    FullName TEXT\r\n);";
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                string sql1 = @"
                                CREATE TABLE IF NOT EXISTS thongtintapbai (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    thongtintapbai TEXT,
                                    vitrichihuy TEXT,
                                    thoigian TEXT,
                                    manh1 TEXT,
                                    manh2 TEXT,
                                    manh3 TEXT,
                                    manh4 TEXT,
                                    tyle TEXT,
                                    nam TEXT,
                                    chihuy_hckt TEXT,
                                    nguoithaythe TEXT,
                                    user TEXT
                                );";
                var command1 = new SQLiteCommand(sql1, connection);
                command1.ExecuteNonQuery(); 
                string sql2 = "CREATE TABLE IF NOT EXISTS quansochiendau (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    phienhieudonvi TEXT,\r\n    phdv1 TEXT,\r\n    phdv2 TEXT,\r\n    phdv3 TEXT,\r\n    phdv4 TEXT,\r\n    phdv5 TEXT,\r\n    quansochiendau TEXT,\r\n    qscd1 TEXT,\r\n    qscd2 TEXT,\r\n    qscd3 TEXT,\r\n    qscd4 TEXT,\r\n    qscd5 TEXT,\r\n    User TEXT\r\n)";
                var command2 = new SQLiteCommand(sql2, connection);
                command2.ExecuteNonQuery();
                string sql3 = "CREATE TABLE IF NOT EXISTS VatChatNguoiDung (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    UserId TEXT NOT NULL,\r\n    vcId INTEGER NOT NULL,\r\n    SoLuong TEXT, \r\n    GhiChu TEXT         \r\n);";
                var command3 = new SQLiteCommand(sql3, connection);
                command3.ExecuteNonQuery();
                string sql4 = "CREATE TABLE IF NOT EXISTS QuyDinhDuTruTieuThuVoSung (\r\n    ID INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    UserId TEXT NOT NULL,\r\n    vcId INTEGER NOT NULL,\r\n    QuyDinhDuTru REAL,\r\n    PhaiCo0400N REAL,\r\n    PhaiCSCD REAL,\r\n    TieuThuGDCB REAL,\r\n    TieuThuGDCD REAL\r\n);";
                var command4 = new SQLiteCommand(sql4, connection);
                command4.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
