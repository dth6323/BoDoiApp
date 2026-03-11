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
                string sql1 = "CREATE TABLE IF NOT EXISTS thongtintepbai (\r\n    id INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    tenvankien TEXT,\r\n    vitrichihuy TEXT,\r\n    thoigian TEXT,\r\n    manh1 TEXT,\r\n    manh2 TEXT,\r\n    manh3 TEXT,\r\n    manh4 TEXT,\r\n    tyle TEXT,\r\n    nam TEXT,\r\n    chihuy_hckt TEXT,\r\n    nguoithaythe TEXT\r\n,User TEXT\r\n );";
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
                    ll TEXT NOT NULL,
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
                    ons TEXT NULL,
                    db TEXT NULL,
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
                    sl INTEGER DEFAULT 0,
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
                var sql11 = @"CREATE TABLE IF NOT EXISTS kehoach_suachua_tbkt (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,

                    loai_tbkt TEXT,

                    ty_le_hu_hong REAL,

                    tong_nhe REAL,
                    tong_vua REAL,
                    tong_nang REAL,
                    tong_huy REAL,
                    tong_cong REAL,

                    kha_nang_nhe REAL,
                    kha_nang_vua REAL,
                    kha_nang_cong REAL,

                    con_lai_nhe REAL,
                    con_lai_vua REAL,
                    con_lai_nang REAL,
                    con_lai_huy REAL,
                    con_lai_cong REAL,

                    User TEXT
                );";
                var command11 = new SQLiteCommand(sql11, connection);
                command11.ExecuteNonQuery();
                var sql12 = @"CREATE TABLE IF NOT EXISTS KhaiBaoTinhHinhVc (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    tt INTEGER,

                    khod REAL,
                    donVi REAL,
                    cong REAL,
                    ghiChu TEXT,

                    User TEXT
                );";
                var command12 = new SQLiteCommand(sql12, connection);
                command12.ExecuteNonQuery();
                var sql13 = @"CREATE TABLE IF NOT EXISTS ChiLenhHkt1 (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    tt INTEGER,

                    qddc REAL,
                    pc04n REAL,
                    pcscd REAL,
                    gdcb REAL,
                    gdcd REAL,

                    User TEXT
                );";
                var command13 = new SQLiteCommand(sql13, connection);
                command13.ExecuteNonQuery();
                var sql14 = @"CREATE TABLE IF NOT EXISTS CanDoi (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    QYDTu TEXT NOT NULL,
    QYDDen TEXT NOT NULL,
    QYETu TEXT NOT NULL,
    QYEDen TEXT NOT NULL,
    TramYTeTu TEXT NOT NULL,
    TramYTeDen TEXT NOT NULL,
    TongTu TEXT NOT NULL,
    TongDen TEXT NOT NULL
);";
                var command14 = new SQLiteCommand(sql14, connection);
                var sql15 = @"CREATE TABLE IF NOT EXISTS du_tinh_khoi_luong (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId TEXT NOT NULL,
                KhoiLuongToanTran REAL NULL,
                KhoiLuongGiaiDoanChuanBi REAL NULL,
                KhoiLuongGiaiDoanChienDau REAL NULL,
                VCHCToanTran REAL NULL,
                VCHCChuanBi REAL NULL,
                VCHCChienDau REAL NULL,
                VCKTToanTran REAL NULL,
                VCKTChuanBi REAL NULL,
                VCKTChienDau REAL NULL
            );";
                var command15 = new SQLiteCommand(sql15, connection);
                command15.ExecuteNonQuery();
                var sql16 = @"
        CREATE TABLE  IF NOT EXISTS candoivt ( Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId TEXT, vtbi_from TEXT, vtbi_to TEXT, vtle_from TEXT, vtle_to TEXT, danquan_from TEXT, danquan_to TEXT, xetho_count TEXT, xetho_from TEXT, xetho_to TEXT, tongkha_from TEXT, tongkha_to TEXT, tong_to TEXT, ketluan TEXT );;";
                var command16 = new SQLiteCommand(sql16, connection);
                command16.ExecuteNonQuery();
                var sql17 = @"
            CREATE TABLE IF NOT EXISTS chhckt (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                User TEXT NOT NULL,

                lienlac1 TEXT,
                lienlac2 TEXT,

                moc1 TEXT,
                moc2 TEXT
        );";
                var command17 = new SQLiteCommand(sql17, connection);
                command17.ExecuteNonQuery();
                var sql18 = @"CREATE TABLE IF NOT EXISTS ToChucSuDungLucLuong(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId TEXT,
            RowIndex INTEGER,
            NoiDung TEXT,
            QS_SQ INTEGER,
            QS_QNCN INTEGER,
            QS_HSQ_BS INTEGER,
            QS_Plus INTEGER,
            VK_VuKhi INTEGER,
            VK_XeMay INTEGER,
            VK_TBKhac INTEGER,
            HC_KT_QS INTEGER,
            HC_KT_TB INTEGER,
            TangCuong_QS INTEGER,
            TangCuong_TB INTEGER);";
                var command18 = new SQLiteCommand(sql18, connection);
                command18.ExecuteNonQuery();
                var sql19 = @"CREATE TABLE IF NOT EXISTS VCHCVTKT
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Row INTEGER,
    Col INTEGER,
    Value TEXT,
    UserId TEXT
);";
                var command19 = new SQLiteCommand(sql19, connection);
                command19.ExecuteNonQuery();
                var sql20 = @"CREATE TABLE IF NOT EXISTS VatChat (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    TT INTEGER,

    LoaiVatChat TEXT,
    DVT TEXT,

    PC_TDQ_KhoD INTEGER DEFAULT 0,
    PC_TDQ_DonVi INTEGER DEFAULT 0,
    PC_TDQ_Plus INTEGER DEFAULT 0,

    PC_SCD_KhoD INTEGER DEFAULT 0,
    PC_SCD_DonVi INTEGER DEFAULT 0,
    PC_SCD_Plus INTEGER DEFAULT 0,

    FOREIGN KEY(UserId) REFERENCES Users(Id)
);";
                var command20 = new SQLiteCommand(sql20, connection);
                command20.ExecuteNonQuery();
                var sql21 = @"CREATE TABLE IF NOT EXISTS kehoachsuachua (
    id INTEGER PRIMARY KEY AUTOINCREMENT,

    loai_tbkt TEXT,
    so_luong REAL,
    ty_le_hu_hong REAL,

    tong_nhe REAL,
    tong_vua REAL,
    tong_nang REAL,
    tong_huy REAL,
    tong_cong REAL,

    kha_nhe REAL,
    kha_vua REAL,
    kha_cong REAL,

    con_nhe REAL,
    con_vua REAL,
    con_nang REAL,
    con_huy REAL,
    con_cong REAL,

    User TEXT
);";
                var command21 = new SQLiteCommand(sql21, connection);
                command21.ExecuteNonQuery();
                var sql22 = @"CREATE TABLE  IF NOT EXISTS KhoiLuongVanTai (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    TT INTEGER,

    B REAL,
    C REAL,
    D REAL,
    E REAL,
    F REAL,
    G REAL,
    H REAL,
    I REAL,
    J REAL,
    K REAL,
    L REAL,

    M TEXT,

    UserId TEXT
);";
                var command22 = new SQLiteCommand(sql22, connection);
                command22.ExecuteNonQuery();
                var sql23 = @"CREATE TABLE IF NOT EXISTS KeHoachVanChuyen (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Row INTEGER,
    Col INTEGER,
    Value TEXT,
    UserId TEXT
);";
                var command23 = new SQLiteCommand(sql23, connection);
                command23.ExecuteNonQuery();
                connection.Close();
            }
            var richTextBoxData = new DataLayer.RichTextBoxData();
            richTextBoxData.CreatTable();
        }
    }
}
