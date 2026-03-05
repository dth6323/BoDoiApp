using BoDoiApp.Resources;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class NhuCauDan : UserControl
    {
        public NhuCauDan()
        {
            InitializeComponent();
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS dan_report (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userId TEXT NOT NULL,
    loai_dan TEXT,
    so_luong_vk INTEGER,
    nhu_cau_co_so INTEGER,
    nhu_cau_tl INTEGER,
    tieu_thu_gdcb_co_so INTEGER,
    tieu_thu_gdcb_tl INTEGER,
    tieu_thu_gdcd_co_so INTEGER,
    tieu_thu_gdcd_tl INTEGER,
    pc_sd_dv_co_so INTEGER,
    pc_sd_kho_co_so INTEGER,
    pc_sd_tl INTEGER,
    hien_co_dv_d INTEGER,
    hien_co_dv_pt INTEGER,
    hien_co_dv_tl INTEGER,
    hien_co_kho_d INTEGER,
    hien_co_kho_pt INTEGER,
    hien_co_kho_tl INTEGER,
    pc_tns_dv_co_so INTEGER,
    pc_tns_kho_co_so INTEGER,
    pc_tns_tl INTEGER,
    kh_truoc_no_sung_dv_d INTEGER,
    kh_truoc_no_sung_dv_pt INTEGER,
    kh_truoc_no_sung_dv_tl INTEGER,
    kh_truoc_no_sung_kho_d INTEGER,
    kh_truoc_no_sung_kho_pt INTEGER,
    kh_truoc_no_sung_kho_tl INTEGER,
    th_no_sung_dv INTEGER,
    th_no_sung_kho INTEGER,
    th_no_sung_tl INTEGER
);";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
