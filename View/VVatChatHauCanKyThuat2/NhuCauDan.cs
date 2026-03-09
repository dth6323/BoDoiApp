using BoDoiApp.Resources;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class NhuCauDan : UserControl
    {
        private string Section { get; set; }
        public NhuCauDan(string section)
        {
            InitializeComponent();
            Section = section;
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS dan_report (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userId TEXT NOT NULL,
    huong TEXT NOT NULL,
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
        private void LoadDanSection(string section, int skipSectionStart, int skipSectionEnd, int startRow, int endRow)
        {
            string sql = "SELECT * FROM dan_report WHERE userId = @userId AND huong = @huong";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@userId", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@huong", section);

                    var reader = command.ExecuteReader();
                    int row = startRow;
                    while (reader.Read())
                    {
                        //if (row >= skipSectionStart && row <= skipSectionEnd)
                        //{
                        //    continue; // Skip rows in the specified range
                        //}
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 0, reader["loai_dan"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 1, reader["so_luong_vk"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 2, reader["nhu_cau_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 3, reader["nhu_cau_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 4, reader["tieu_thu_gdcb_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 5, reader["tieu_thu_gdcb_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 6, reader["tieu_thu_gdcd_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 7, reader["tieu_thu_gdcd_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 8, reader["pc_sd_dv_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 9, reader["pc_sd_kho_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 10, reader["pc_sd_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 11, reader["hien_co_dv_d"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 12, reader["hien_co_dv_pt"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 13, reader["hien_co_dv_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 14, reader["hien_co_kho_d"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 15, reader["hien_co_kho_pt"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 16, reader["hien_co_kho_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 17, reader["pc_tns_dv_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 18, reader["pc_tns_kho_co_so"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 19, reader["pc_tns_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 20, reader["kh_truoc_no_sung_dv_d"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 21, reader["kh_truoc_no_sung_dv_pt"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 22, reader["kh_truoc_no_sung_dv_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 23, reader["kh_truoc_no_sung_kho_d"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 24, reader["kh_truoc_no_sung_kho_pt"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 25, reader["kh_truoc_no_sung_kho_tl"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 26, reader["th_no_sung_dv"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 27, reader["th_no_sung_kho"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 28, reader["th_no_sung_tl"].ToString());
                        row++;
                    }
                }
            }

        }
        private void SaveDanSection(string section, int startRow, int endRow)
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                for (int row = startRow; row <= endRow; row++)
                {
                    string loaiDan = reoGridControl1.CurrentWorksheet.GetCellData(row, 0)?.ToString() ?? "";

                    // Check if record exists
                    string checkSql = "SELECT COUNT(*) FROM dan_report WHERE userId = @userId AND huong = @huong AND loai_dan = @loaiDan";
                    bool exists = false;

                    using (var checkCommand = new SQLiteCommand(checkSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@userId", Constants.CURRENT_USER_ID_VALUE);
                        checkCommand.Parameters.AddWithValue("@huong", section);
                        checkCommand.Parameters.AddWithValue("@loaiDan", loaiDan);
                        exists = (long)checkCommand.ExecuteScalar() > 0;
                    }

                    string sql;
                    if (exists)
                    {
                        sql = @"UPDATE dan_report SET 
                            so_luong_vk = @soLuongVk,
                            nhu_cau_co_so = @nhuCauCoSo,
                            nhu_cau_tl = @nhuCauTl,
                            tieu_thu_gdcb_co_so = @tieuThuGdcbCoSo,
                            tieu_thu_gdcb_tl = @tieuThuGdcbTl,
                            tieu_thu_gdcd_co_so = @tieuThuGdcdCoSo,
                            tieu_thu_gdcd_tl = @tieuThuGdcdTl,
                            pc_sd_dv_co_so = @pcSdDvCoSo,
                            pc_sd_kho_co_so = @pcSdKhoCoSo,
                            pc_sd_tl = @pcSdTl,
                            hien_co_dv_d = @hienCoDvD,
                            hien_co_dv_pt = @hienCoDvPt,
                            hien_co_dv_tl = @hienCoDvTl,
                            hien_co_kho_d = @hienCoKhoD,
                            hien_co_kho_pt = @hienCoKhoPt,
                            hien_co_kho_tl = @hienCoKhoTl,
                            pc_tns_dv_co_so = @pcTnsDvCoSo,
                            pc_tns_kho_co_so = @pcTnsKhoCoSo,
                            pc_tns_tl = @pcTnsTl,
                            kh_truoc_no_sung_dv_d = @khTruocNoSungDvD,
                            kh_truoc_no_sung_dv_pt = @khTruocNoSungDvPt,
                            kh_truoc_no_sung_dv_tl = @khTruocNoSungDvTl,
                            kh_truoc_no_sung_kho_d = @khTruocNoSungKhoD,
                            kh_truoc_no_sung_kho_pt = @khTruocNoSungKhoPt,
                            kh_truoc_no_sung_kho_tl = @khTruocNoSungKhoTl,
                            th_no_sung_dv = @thNoSungDv,
                            th_no_sung_kho = @thNoSungKho,
                            th_no_sung_tl = @thNoSungTl
                            WHERE userId = @userId AND huong = @huong AND loai_dan = @loaiDan";
                    }
                    else
                    {
                        sql = @"INSERT INTO dan_report (userId, huong, loai_dan, so_luong_vk, nhu_cau_co_so, nhu_cau_tl, 
                            tieu_thu_gdcb_co_so, tieu_thu_gdcb_tl, tieu_thu_gdcd_co_so, tieu_thu_gdcd_tl, 
                            pc_sd_dv_co_so, pc_sd_kho_co_so, pc_sd_tl, hien_co_dv_d, hien_co_dv_pt, hien_co_dv_tl, 
                            hien_co_kho_d, hien_co_kho_pt, hien_co_kho_tl, pc_tns_dv_co_so, pc_tns_kho_co_so, pc_tns_tl, 
                            kh_truoc_no_sung_dv_d, kh_truoc_no_sung_dv_pt, kh_truoc_no_sung_dv_tl, kh_truoc_no_sung_kho_d, 
                            kh_truoc_no_sung_kho_pt, kh_truoc_no_sung_kho_tl, th_no_sung_dv, th_no_sung_kho, th_no_sung_tl)
                            VALUES (@userId, @huong, @loaiDan, @soLuongVk, @nhuCauCoSo, @nhuCauTl, @tieuThuGdcbCoSo, 
                            @tieuThuGdcbTl, @tieuThuGdcdCoSo, @tieuThuGdcdTl, @pcSdDvCoSo, @pcSdKhoCoSo, @pcSdTl, 
                            @hienCoDvD, @hienCoDvPt, @hienCoDvTl, @hienCoKhoD, @hienCoKhoPt, @hienCoKhoTl, @pcTnsDvCoSo, 
                            @pcTnsKhoCoSo, @pcTnsTl, @khTruocNoSungDvD, @khTruocNoSungDvPt, @khTruocNoSungDvTl, 
                            @khTruocNoSungKhoD, @khTruocNoSungKhoPt, @khTruocNoSungKhoTl, @thNoSungDv, @thNoSungKho, @thNoSungTl)";
                    }

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userId", Constants.CURRENT_USER_ID_VALUE);
                        command.Parameters.AddWithValue("@huong", section);
                        command.Parameters.AddWithValue("@loaiDan", loaiDan);
                        command.Parameters.AddWithValue("@soLuongVk", reoGridControl1.CurrentWorksheet.GetCellData(row, 1)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@nhuCauCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 2)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@nhuCauTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 3)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@tieuThuGdcbCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 4)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@tieuThuGdcbTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 5)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@tieuThuGdcdCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 6)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@tieuThuGdcdTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 7)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcSdDvCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 8)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcSdKhoCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 9)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcSdTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 10)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoDvD", reoGridControl1.CurrentWorksheet.GetCellData(row, 11)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoDvPt", reoGridControl1.CurrentWorksheet.GetCellData(row, 12)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoDvTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 13)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoKhoD", reoGridControl1.CurrentWorksheet.GetCellData(row, 14)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoKhoPt", reoGridControl1.CurrentWorksheet.GetCellData(row, 15)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@hienCoKhoTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 16)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcTnsDvCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 17)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcTnsKhoCoSo", reoGridControl1.CurrentWorksheet.GetCellData(row, 18)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@pcTnsTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 19)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungDvD", reoGridControl1.CurrentWorksheet.GetCellData(row, 20)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungDvPt", reoGridControl1.CurrentWorksheet.GetCellData(row, 21)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungDvTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 22)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungKhoD", reoGridControl1.CurrentWorksheet.GetCellData(row, 23)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungKhoPt", reoGridControl1.CurrentWorksheet.GetCellData(row, 24)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@khTruocNoSungKhoTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 25)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@thNoSungDv", reoGridControl1.CurrentWorksheet.GetCellData(row, 26)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@thNoSungKho", reoGridControl1.CurrentWorksheet.GetCellData(row, 27)?.ToString() ?? "");
                        command.Parameters.AddWithValue("@thNoSungTl", reoGridControl1.CurrentWorksheet.GetCellData(row, 28)?.ToString() ?? "");

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(()=>new Form1());
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn lưu dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                switch (Section)
                {
                    case "Toàn d":
                        SaveDanSection(Section, 27, 40);
                        break;
                    case "Hướng chủ yếu":
                        SaveDanSection(Section, 43, 55);
                        break;
                    case "Hướng thứ yếu":
                        SaveDanSection(Section, 57, 70);
                        break;
                    case "BP PNPS":
                        SaveDanSection(Section, 73, 85);
                        break;
                    case "LL còn lại":
                        SaveDanSection(Section, 87, 100);
                        break;
                    default:
                        MessageBox.Show("Lỗi: Không xác định được phần dữ liệu để lưu.");
                        return;
                }
                MessageBox.Show("Dữ liệu đã được lưu thành công.");

            }
        }

        private void NhuCauDan_Load(object sender, System.EventArgs e)
        {

            reoGridControl1.Load(@"D:\Downloads\Đạn.xlsx");
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["Dan"];
            reoGridControl1.CurrentWorksheet.HideColumns(29, 22);
            switch (Section)
            {
                case "Toàn d":
                    LoadDanSection(Section,-1,-1, 27, 40);
                    reoGridControl1.CurrentWorksheet.HideRows(42,60);
                    break;
                case "Hướng chủ yếu":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 14);
                    reoGridControl1.CurrentWorksheet.HideRows(57, 45);
                    LoadDanSection(Section, -1, -1, 43, 55);
                    break;
                case "Hướng thứ yếu":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 29);
                    reoGridControl1.CurrentWorksheet.HideRows(72, 30);

                    LoadDanSection(Section, -1,-1, 57, 70);
                    break;
                case "BP PNPS":
                    reoGridControl1.CurrentWorksheet.HideRows(27,45);
                    reoGridControl1.CurrentWorksheet.HideRows(87, 14);

                    LoadDanSection(Section, -1,-1, 73, 85);
                    break;
                case "LL còn lại":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 60);

                    LoadDanSection(Section, -1,-1, 87, 100);
                    break;
                default:
                    MessageBox.Show("Lỗi: Không xác định được phần dữ liệu để lưu.");
                    return;
            }
        }
    }
}
