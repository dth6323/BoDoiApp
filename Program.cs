using BoDoiApp.View;
using BoDoiApp.View.VIBaoDamSinhHoat;
using BoDoiApp.View.VIIBaoDamQuanY;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BoDoiApp
{
    internal static class Program
    {


        private const string connectionString = "Data Source=data.db;Version=3;";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        [STAThread]
        static void Main()
        {
            AllocConsole(); // mở console

            Console.WriteLine("Console đã mở!");
            InitializeDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMana.Init();
            Application.Run(new MainForm());
        }

        private static void InitializeDatabase()
        {


            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "CREATE TABLE IF NOT EXISTS Users (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    Username TEXT NOT NULL UNIQUE,\r\n    Password TEXT NOT NULL,\r\n    FullName TEXT\r\n);";
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                string sql1 = "CREATE TABLE IF NOT EXISTS thongtintepbai (\r\n    Id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    tendaubai TEXT,\r\n    sochuy TEXT,\r\n    bandotapbai TEXT,\r\n    manh1 TEXT,\r\n    manh2 TEXT,\r\n    manh3 TEXT,\r\n    manh4 TEXT,\r\n    chihuyduan TEXT,\r\n    chihuyhaucan TEXT,\r\n    chihuyduan_tt TEXT,\r\n    chihuyhaucan_tt TEXT,\r\n    captren TEXT,\r\n    capminh TEXT,\r\n    User TEXT\r\n);";
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
                var sql5 = @"CREATE TABLE IF NOT EXISTS trangkithuat (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    quan_so TEXT NOT NULL, 
                    sn TEXT NULL, 
                    tl TEXT NULL, 
                    trl TEXT NULL, 
                    dl TEXT NULL, 
                    b41_m79 TEXT NULL, 
                    luu_dan TEXT NULL, 
                    coi_60 TEXT NULL, 
                    coi_82 TEXT NULL, 
                    coi_100 TEXT NULL, 
                    pct_spg9 TEXT NULL, 
                    phao_pk_127 TEXT NULL, 
                    User TEXT NULL, 
                    option TEXT NOT NULL
                );";
                var command5 = new SQLiteCommand(sql5, connection);
                command5.ExecuteNonQuery();
                var sql6 = @"CREATE TABLE IF NOT EXISTS baodamsinhhoat (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    noidung TEXT,
                    loai TEXT,
                    User TEXT
                );";
                var command6 = new SQLiteCommand(sql6, connection);
                command6.ExecuteNonQuery();
                var sql7 = @"CREATE TABLE IF NOT EXISTS baoduongsuachua (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    noidung TEXT,
                    loai TEXT,
                    User TEXT
                );";
                var command7 = new SQLiteCommand(sql7, connection);
                command7.ExecuteNonQuery();
                var sql8 = @"CREATE TABLE IF NOT EXISTS suachua_tbkt (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ty_le_hu_hong REAL DEFAULT 0, 
                    nhe INTEGER DEFAULT 0,
                    vua INTEGER DEFAULT 0,
                    nang INTEGER DEFAULT 0,
                    huy INTEGER DEFAULT 0,
                    cong INTEGER DEFAULT 0, 
                    User TEXT NOT NULL
                );";
                var command8 = new SQLiteCommand(sql8, connection);
                command8.ExecuteNonQuery();
                var sql9 = @"CREATE TABLE IF NOT EXISTS baodam_quany (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    quan_so REAL,
                    tb_qs REAL,
                    tb_nguoi REAL,
                    tbhh_qs REAL,
                    tbhh_nguoi REAL,
                    bb_qs REAL,
                    bb_nguoi REAL,
                    cong_nguoi REAL,
                    User TEXT
                );";
                var command9 = new SQLiteCommand(sql9, connection);
                command9.ExecuteNonQuery();
                var sql10 = @"CREATE TABLE IF NOT EXISTS kehoach_baodam_quany (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    quan_so REAL,
                    tb_qs REAL,
                    tb_nguoi REAL,
                    tbhh_qs REAL,
                    tbhh_nguoi REAL,
                    bb_qs REAL,
                    bb_nguoi REAL,
                    cong_nguoi REAL,
                    cang_bo REAL,
                    tu_di REAL,
                    tong REAL,
                    User TEXT
                );";
                var command10 = new SQLiteCommand(sql10, connection);
                command10.ExecuteNonQuery();
                connection.Close();
            }
            var richTextBoxData = new DataLayer.RichTextBoxData();
            richTextBoxData.CreatTable();
        }
    }
}
