using BoDoiApp.DataLayer;
using BoDoiApp.DataLayer.KhaiBao;
using BoDoiApp.Resources;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class NhuCauDan : UserControl
    {

        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "DANTEST.xlsx");
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
    tt INTEGER,
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
        private void LockSheetAndOpenCells(Worksheet ws)
        {
            // 1. Lock toàn bộ sheet
            ws.Ranges[new RangePosition(0, 0, ws.RowCount, ws.ColumnCount)].IsReadonly = true;

            // 2. Mở I,M,P,R từ dòng 28 → 41
            int[] cols1 = { 8, 12, 15, 17 }; // I,M,P,R

            for (int r = 28; r <= 41; r++)
            {
                foreach (var c in cols1)
                {
                    ws.Cells[r, c].IsReadOnly = false;
                }
            }

            // 3. Mở G,I,L,R ở các dòng 56,71,86,101
            int[] rows2 = { 56, 71, 86, 101 };
            int[] cols2 = { 6, 8, 11, 17 }; // G,I,L,R

            foreach (var r in rows2)
            {
                foreach (var c in cols2)
                {
                    ws.Cells[r, c].IsReadOnly = false;
                }
            }
        }
        private void LoadDanSection(string section, int skipSectionStart, int skipSectionEnd, int startRow, int endRow)
        {
            var ws = reoGridControl1.CurrentWorksheet;

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
                        void SetIfUnlocked(int col, object value)
                        {
                            if (!ws.Cells[row, col].IsReadOnly)
                            {
                                ws.SetCellData(row, col, value?.ToString());
                            }
                        }

                        SetIfUnlocked(0, reader["loai_dan"]);
                        SetIfUnlocked(1, reader["so_luong_vk"]);
                        SetIfUnlocked(2, reader["nhu_cau_co_so"]);
                        SetIfUnlocked(3, reader["nhu_cau_tl"]);
                        SetIfUnlocked(4, reader["tieu_thu_gdcb_co_so"]);
                        SetIfUnlocked(5, reader["tieu_thu_gdcb_tl"]);
                        SetIfUnlocked(6, reader["tieu_thu_gdcd_co_so"]);
                        SetIfUnlocked(7, reader["tieu_thu_gdcd_tl"]);
                        SetIfUnlocked(8, reader["pc_sd_dv_co_so"]);
                        SetIfUnlocked(9, reader["pc_sd_kho_co_so"]);
                        SetIfUnlocked(10, reader["pc_sd_tl"]);
                        SetIfUnlocked(11, reader["hien_co_dv_d"]);
                        SetIfUnlocked(12, reader["hien_co_dv_pt"]);
                        SetIfUnlocked(13, reader["hien_co_dv_tl"]);
                        SetIfUnlocked(14, reader["hien_co_kho_d"]);
                        SetIfUnlocked(15, reader["hien_co_kho_pt"]);
                        SetIfUnlocked(16, reader["hien_co_kho_tl"]);
                        SetIfUnlocked(17, reader["pc_tns_dv_co_so"]);
                        SetIfUnlocked(18, reader["pc_tns_kho_co_so"]);
                        SetIfUnlocked(19, reader["pc_tns_tl"]);
                        SetIfUnlocked(20, reader["kh_truoc_no_sung_dv_d"]);
                        SetIfUnlocked(21, reader["kh_truoc_no_sung_dv_pt"]);
                        SetIfUnlocked(22, reader["kh_truoc_no_sung_dv_tl"]);
                        SetIfUnlocked(23, reader["kh_truoc_no_sung_kho_d"]);
                        SetIfUnlocked(24, reader["kh_truoc_no_sung_kho_pt"]);
                        SetIfUnlocked(25, reader["kh_truoc_no_sung_kho_tl"]);
                        SetIfUnlocked(26, reader["th_no_sung_dv"]);
                        SetIfUnlocked(27, reader["th_no_sung_kho"]);
                        SetIfUnlocked(28, reader["th_no_sung_tl"]);
                        row++;
                    }
                }
            }

        }
        private void SaveDanSection(string section, int startRow, int endRow)
        {
            int offsetTT = 0;

            switch (section)
            {
                case "Toàn d": offsetTT = 0; break;
                case "Hướng chủ yếu": offsetTT = 14; break;
                case "Hướng thứ yếu": offsetTT = 28; break;
                case "BP PNPS": offsetTT = 43; break;
                case "LL còn lại": offsetTT = 58; break;
            }
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                for (int row = startRow; row <= endRow; row++)
                {
                    int tt = offsetTT + (row - startRow + 1);

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
                            tt = @tt,
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
                        sql = @"INSERT INTO dan_report (tt,userId, huong, loai_dan, so_luong_vk, nhu_cau_co_so, nhu_cau_tl, 
                            tieu_thu_gdcb_co_so, tieu_thu_gdcb_tl, tieu_thu_gdcd_co_so, tieu_thu_gdcd_tl, 
                            pc_sd_dv_co_so, pc_sd_kho_co_so, pc_sd_tl, hien_co_dv_d, hien_co_dv_pt, hien_co_dv_tl, 
                            hien_co_kho_d, hien_co_kho_pt, hien_co_kho_tl, pc_tns_dv_co_so, pc_tns_kho_co_so, pc_tns_tl, 
                            kh_truoc_no_sung_dv_d, kh_truoc_no_sung_dv_pt, kh_truoc_no_sung_dv_tl, kh_truoc_no_sung_kho_d, 
                            kh_truoc_no_sung_kho_pt, kh_truoc_no_sung_kho_tl, th_no_sung_dv, th_no_sung_kho, th_no_sung_tl)
                            VALUES (@tt,@userId, @huong, @loaiDan, @soLuongVk, @nhuCauCoSo, @nhuCauTl, @tieuThuGdcbCoSo, 
                            @tieuThuGdcbTl, @tieuThuGdcdCoSo, @tieuThuGdcdTl, @pcSdDvCoSo, @pcSdKhoCoSo, @pcSdTl, 
                            @hienCoDvD, @hienCoDvPt, @hienCoDvTl, @hienCoKhoD, @hienCoKhoPt, @hienCoKhoTl, @pcTnsDvCoSo, 
                            @pcTnsKhoCoSo, @pcTnsTl, @khTruocNoSungDvD, @khTruocNoSungDvPt, @khTruocNoSungDvTl, 
                            @khTruocNoSungKhoD, @khTruocNoSungKhoPt, @khTruocNoSungKhoTl, @thNoSungDv, @thNoSungKho, @thNoSungTl)";
                    }
                    
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tt", tt);
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
                        SaveDanSection(Section, 42, 55);
                        break;
                    case "Hướng thứ yếu":
                        SaveDanSection(Section, 57, 70);
                        break;
                    case "BP PNPS":
                        SaveDanSection(Section, 72, 85);
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

            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["QSTBKT"];
            VCHCVTKTDATA.LoadTrangKiThuat(reoGridControl1);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["TinhHinhVC"];
            TinhHinhVcData.LoadAllCell(reoGridControl1);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["ChiLenhHCKT"];
            ChiLenhHKT1Data.LoadAllCell(reoGridControl1);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["Dan"];
            DanData.LoadAllCell(reoGridControl1, "Toàn d");
            DanData.LoadAllCell(reoGridControl1, "Hướng chủ yếu");
            DanData.LoadAllCell(reoGridControl1, "Hướng thứ yếu");
            DanData.LoadAllCell(reoGridControl1, "BP PNPS");
            DanData.LoadAllCell(reoGridControl1, "LL còn lại");
            reoGridControl1.CurrentWorksheet.HideColumns(29, 22);
            var ws = reoGridControl1.CurrentWorksheet;
            LockSheetAndOpenCells(ws);
            reoGridControl1.SheetTabVisible = false;
            switch (Section)
            {
                case "Toàn d":
                    LoadDanSection(Section,-1,-1, 27, 40);
                    reoGridControl1.CurrentWorksheet.HideRows(42,60);

                    ws.HideColumns(61, ws.ColumnCount - 61);

                    ws.HideRows(41, ws.RowCount - 41);
                    break;
                case "Hướng chủ yếu":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 14);
                    reoGridControl1.CurrentWorksheet.HideRows(57, 45);
                    LoadDanSection(Section, -1, -1, 42, 55);
                    ws.HideColumns(61, ws.ColumnCount - 61);
                    ws.HideRows(56, ws.RowCount - 56);
                    break;
                case "Hướng thứ yếu":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 29);
                    reoGridControl1.CurrentWorksheet.HideRows(72, 30);

                    LoadDanSection(Section, -1,-1, 57, 70);
                    ws.HideRows(71, ws.RowCount - 71);
                    ws.HideColumns(61, ws.ColumnCount - 61);
                    break;
                case "BP PNPS":
                    reoGridControl1.CurrentWorksheet.HideRows(27,45);
                    reoGridControl1.CurrentWorksheet.HideRows(87, 14);

                    LoadDanSection(Section, -1,-1, 72, 85);
                    ws.HideRows(86, ws.RowCount - 86);
                    ws.HideColumns(61, ws.ColumnCount - 61);
                    break;
                case "LL còn lại":
                    reoGridControl1.CurrentWorksheet.HideRows(27, 60);

                    LoadDanSection(Section, -1,-1, 87, 100);
                    ws.HideRows(101, ws.RowCount - 101);
                    ws.HideColumns(61, ws.ColumnCount - 61);
                    break;
                default:
                    MessageBox.Show("Lỗi: Không xác định được phần dữ liệu để lưu.");
                    return;
            }
        }
    }
}
