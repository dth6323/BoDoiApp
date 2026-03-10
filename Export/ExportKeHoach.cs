using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace BoDoiApp.Export
{
    internal class ExportKeHoach
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();

        private const string connectionString = "Data Source=data.db;Version=3;";
        private readonly BaoDamSinhHoatData dataLayer6 = new BaoDamSinhHoatData();

        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private readonly string template =
            Path.Combine(BaseDir, "Resources", "word", "word2.docx");

        public void ExportWord()
        {
            string output = @"D:\KEHOACH.docx";

            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // ===== Query 1 =====
                data["{{12}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TinhHinhTacDong") ?? "";
                data["{{13}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "NhiemVu") ?? "";

                // ===== Query 2 =====
                var cmd4 = new SQLiteCommand(
                    "SELECT * FROM tbkt_supply_plan WHERE UserId = @UserId",
                    connection
                );

                cmd4.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index = 85;

                using (var reader = cmd4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index++}}}}}"] = reader["nhu_cau"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["hien_co_tong_so"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["hien_co_so_tot"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["hien_co_kbq"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["hien_co_kt"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["phai_co_truoc_cd"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["bo_sung_so_luong"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["bo_sung_thoi_gian"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["bo_sung_dia_diem"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["bo_sung_phuong_thuc"]?.ToString() ?? "";
                    }
                }

                data["{{534}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapBaoDam2") ?? "";

                // ===== 5.1 =====
                var cmd5 = new SQLiteCommand(
                    "Select * from dan_report WHERE UserId = @UserId ORDER BY tt ASC",
                    connection
                );

                cmd5.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index2 = 535;

                using (var reader = cmd5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index2++}}}}}"] = reader["so_luong_vk"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["nhu_cau_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["nhu_cau_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["tieu_thu_gdcb_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["tieu_thu_gdcb_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["tieu_thu_gdcd_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["tieu_thu_gdcd_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_sd_dv_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_sd_kho_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_sd_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_dv_d"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_dv_pt"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_dv_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_kho_d"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_kho_pt"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["hien_co_kho_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_tns_dv_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_tns_kho_co_so"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["pc_tns_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_dv_d"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_dv_pt"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_dv_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_kho_d"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_kho_pt"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["kh_truoc_no_sung_kho_tl"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["th_no_sung_dv"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["th_no_sung_kho"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["th_no_sung_tl"]?.ToString() ?? "";
                    }
                }
                data["{{4520}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapDamBao") ?? "";
                data["{{4521}}"] = dataLayer6.LayThongTin("BaoDamSinhHoat_AnUong");
                data["{{4522"] = dataLayer6.LayThongTin("BaoDamSinhHoat_Mac");
                data["{{4523}}"] = dataLayer6.LayThongTin("BaoDamSinhHoat_ONghi");
                // ===== 5.2 =====

                var cmd6 = new SQLiteCommand(
                    @"SELECT Row,Col,Value
                      FROM VCHCVTKT
                      WHERE UserId=@User
                      ORDER BY Row ASC, Col ASC",
                    connection);

                cmd6.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);

                int index4 = 2495;

                using (var reader = cmd6.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index4++}}}}}"] = reader["Value"]?.ToString() ?? "";
                    }
                }

                ////6.1
                var cmd7 = new SQLiteCommand(
                    @"SELECT * FROM kehoach_baodam_quany WHERE User=@User",
                    connection
                );

                cmd7.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);

                int index3 = 4524;

                using (var reader = cmd7.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        data[$"{{{{{index3++}}}}}"] = reader["quan_so"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tb_qs"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tb_nguoi"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tbhh_qs"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tbhh_nguoi"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["bb_qs"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["bb_nguoi"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["cong_nguoi"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["cang_bo"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tu_di"]?.ToString() ?? "";
                        data[$"{{{{{index3++}}}}}"] = reader["tong"]?.ToString() ?? "";
                    }
                }
                data["{{4572}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "_3YDinh") ?? "";
                data["{{4573}}"] = dataLayer6.LayThongTin("BaoDuongSuaChua_1");


                //data["{{45}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "_3YDinh") ?? "";
                //data["{{46}}"] = dataLayer6.LayThongTin("BaoDuongSuaChua_1");

                var cmd9 = new SQLiteCommand("SELECT * FROM kehoachsuachua WHERE User=@User", connection);
                cmd9.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                using (var reader = cmd9.ExecuteReader())
                {
                    int i = 4574;
                    while (reader.Read())
                    {
                        data["{{" + i++ + "}}"] = reader["so_luong"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["ty_le_hu_hong"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["tong_nhe"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["tong_vua"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["tong_nang"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["tong_huy"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["tong_cong"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["kha_nhe"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["kha_vua"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["kha_cong"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["con_nhe"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["con_vua"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["con_nang"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["con_huy"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["con_cong"]?.ToString() ?? "";
                    }
                }

                data["{{4714}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapSuaChua") ?? "";
                
                data["{{4715}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai1") ?? "";
                data["{{4716}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai2") ?? "";
                data["{{4717}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai3") ?? "";
                data["{{4718}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai4") ?? "";

                var cmd10 = new SQLiteCommand("SELECT * FROM KhoiLuongVanTai WHERE UserId=@User ORDER BY TT", connection);
                cmd10.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                using (var reader = cmd10.ExecuteReader())
                {
                    int i = 4719;
                    while (reader.Read())
                    {
                        data["{{" + i++ + "}}"] = reader["B"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["C"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["D"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["E"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["F"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["G"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["H"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["I"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["J"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["K"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["L"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["M"]?.ToString() ?? "";
                         }
                }
                var cmd11 = new SQLiteCommand(
                    @"SELECT Row,Col,Value
                      FROM KeHoachVanChuyen
                      WHERE UserId=@User
                      ORDER BY Row ASC, Col ASC",
                    connection);

                cmd11.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);


                int index7 = 4826;
                using (var reader = cmd6.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        data[$"{{{{{index7++}}}}}"] = reader["Value"]?.ToString() ?? "";
                    }
                }
                data["{{5226}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "IX_4_YdinhVanChuyen") ?? "";
                data["{{5227}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "X_DuKien") ?? "";
                data["{{5228}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "X_BienPhap") ?? "";
                var cmd12 = new SQLiteCommand(
                    "SELECT chihuy_hckt, nguoithaythe FROM thongtintepbai   WHERE User= @UserId",
                    connection
                );

                cmd12.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd12.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{5229}}"] = reader["chihuy_hckt"]?.ToString() ?? "";
                    }
                }
                var cmd13 = new SQLiteCommand(
                    @"
                    SELECT lienlac1,lienlac2,moc1,moc2
                    FROM chhckt
                    WHERE User=@User
                    LIMIT 1",
                    connection
                );

                cmd13.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd13.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{5230}}"] = reader["lienlac1"]?.ToString() ?? "";
                        data["{{5231}}"] = reader["moc1"]?.ToString() ?? "";
                    }
                }
                var cmd14 = new SQLiteCommand(
                    @"
                    SELECT * from Users
                    WHERE Username=@User
                    LIMIT 1",
                    connection
                );

                cmd14.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd13.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{5232}}"] = reader["FullName"]?.ToString() ?? "";
                        data["{{11}}"] = reader["FullName"]?.ToString() ?? "";
                    }
                }
                var cmd15 = new SQLiteCommand(
                    @"
                    SELECT * from thongtintepbai
                    WHERE User=@User
                    LIMIT 1",
                    connection
                );

                cmd15.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd15.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{2}}"] = reader["tenvankien"]?.ToString() ?? "";
                        data["{{3}}"] = reader["vitrichihuy"]?.ToString() ?? "";
                        data["{{4}}"] = reader["thoigian"]?.ToString() ?? "";
                        data["{{7}}"] = reader["manh1"]?.ToString() ?? "";
                        data["{{8}}"] = reader["manh2"]?.ToString() ?? "";
                        data["{{9}}"] = reader["manh3"]?.ToString() ?? "";
                        data["{{10}}"] = reader["manh4"]?.ToString() ?? "";
                        data["{{5}}"] = reader["tyle"]?.ToString() ?? "";
                        data["{{6}}"] = reader["nam"]?.ToString() ?? "";
                    }
                }
                //data["{{74}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "XI_Ketluan") ?? "";
                //data["{{75}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "XI_Denghi") ?? "";

                ExportFromTemplate(template, output, data);

                MessageBox.Show("Xuất file thành công!");
            }
        }

        public void ExportFromTemplate(
    string templatePath,
    string outputPath,
    Dictionary<string, string> data
)
        {
            File.Copy(templatePath, outputPath, true);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
            {
                var paragraphs = doc.MainDocumentPart.Document.Descendants<Paragraph>();

                foreach (var paragraph in paragraphs)
                {
                    var texts = paragraph.Descendants<Text>().ToList();
                    if (texts.Count == 0) continue;

                    string combinedText = string.Join("", texts.Select(t => t.Text));

                    // replace theo dictionary
                    foreach (var item in data)
                    {
                        combinedText = combinedText.Replace(item.Key, item.Value ?? "");
                    }

                    // ===== XÓA placeholder chưa replace =====
                    combinedText = Regex.Replace(combinedText, @"\{\{\d+\}\}", "");

                    texts[0].Text = combinedText;

                    for (int i = 1; i < texts.Count; i++)
                    {
                        texts[i].Text = "";
                    }
                }

                doc.MainDocumentPart.Document.Save();
            }
        }
    }
}
