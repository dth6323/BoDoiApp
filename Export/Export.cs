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
    internal class WordExporter
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();

        private const string connectionString = "Data Source=data.db;Version=3;";
        private readonly BaoDamSinhHoatData dataLayer6 = new BaoDamSinhHoatData();

        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private readonly string template =
            Path.Combine(BaseDir, "Resources", "word", "word1.docx");

        public void ExportWord()
        {
            string output = @"D:\word2.docx";

            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // ===== Query 1 =====
                data["{{1}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TinhHinhTacDong") ?? "";
                data["{{2}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "NhiemVu") ?? "";

                var cmd3 = new SQLiteCommand(
                    "SELECT ToChuc, BoTri FROM ToChucSuDungBoTri WHERE UserId = @UserId",
                    connection
                );

                cmd3.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd3.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{3}}"] = reader["ToChuc"]?.ToString() ?? "";
                        data["{{4}}"] = reader["BoTri"]?.ToString() ?? "";
                    }
                }

                // ===== Query 2 =====
                var cmd4 = new SQLiteCommand(
                    "SELECT NhuCau,HienCo_Plus,HienCo_Kbd,HienCo_Kt,HienCo_SoTot,PCTQD,BoSung FROM \"4_1_ChiTieu\" WHERE UserId = @UserId",
                    connection
                );

                cmd4.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index = 600;

                using (var reader = cmd4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index++}}}}}"] = reader["NhuCau"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["HienCo_Plus"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["HienCo_Kbd"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["HienCo_Kt"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["HienCo_SoTot"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["PCTQD"]?.ToString() ?? "";
                        data[$"{{{{{index++}}}}}"] = reader["BoSung"]?.ToString() ?? "";
                    }
                }

                data["{{6}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TiepNhanBoSung") ?? "";

                // ===== 5.1 =====
                var cmd5 = new SQLiteCommand(
                    "Select QuyDinh,HienCo,BOSUNG from \"5_1_VatTu\" WHERE UserId = @UserId",
                    connection
                );

                cmd5.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index2 = 7;

                using (var reader = cmd5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index2++}}}}}"] = reader["QuyDinh"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["HienCo"]?.ToString() ?? "";
                        data[$"{{{{{index2++}}}}}"] = reader["BOSUNG"]?.ToString() ?? "";
                    }
                }

                // ===== 5.2 =====
                var cmd6 = new SQLiteCommand(
                    @"SELECT
                        TT,
                        LoaiVatChat,
                        DVT,
                        PC_TDQ_KhoD,
                        PC_TDQ_DonVi,
                        PC_TDQ_Plus,
                        PC_SCD_KhoD,
                        PC_SCD_DonVi,
                        PC_SCD_Plus
                    FROM VatChat
                    WHERE UserId = @UserId
                    ORDER BY TT ASC;",
                    connection
                );

                cmd6.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index4 = 111;

                using (var reader = cmd6.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[$"{{{{{index4++}}}}}"] = reader["PC_TDQ_KhoD"]?.ToString() ?? "";
                        data[$"{{{{{index4++}}}}}"] = reader["PC_TDQ_DonVi"]?.ToString() ?? "";
                        data[$"{{{{{index4++}}}}}"] = reader["PC_TDQ_Plus"]?.ToString() ?? "";
                        data[$"{{{{{index4++}}}}}"] = reader["PC_SCD_KhoD"]?.ToString() ?? "";
                        data[$"{{{{{index4++}}}}}"] = reader["PC_SCD_DonVi"]?.ToString() ?? "";
                        data[$"{{{{{index4++}}}}}"] = reader["PC_SCD_Plus"]?.ToString() ?? "";
                    }
                }
                //5.3 
                data["{{26}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TiepNhanBoXungV") ?? "";
                data["{{27}}"] = dataLayer6.LayThongTin("BaoDamSinhHoat_AnUong");
                data["{{27}}"] = dataLayer6.LayThongTin("BaoDamSinhHoat_Mac");
                data["{{27}}"] = dataLayer6.LayThongTin("BaoDamSinhHoat_ONghi");

                //6.1
                var cmd7 = new SQLiteCommand(
                    @"SELECT
                        quan_so ,tb_qs ,tb_nguoi ,tbhh_qs ,tbhh_nguoi ,bb_qs ,bb_nguoi ,cong_nguoi
                        FROM baodam_quany 
                    WHERE User = @UserId",
                    connection
                );

                cmd7.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                int index3 = 177;

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
                    }
                }
                var cmd8 = new SQLiteCommand(
                        "SELECT * FROM CanDoi WHERE UserId = @UserId LIMIT 1;",
                        connection
                    );

                cmd8.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd8.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{33}}"] = reader["QYDTu"]?.ToString() ?? "";
                        data["{{34}}"] = reader["QYDDen"]?.ToString() ?? "";
                        data["{{35}}"] = reader["QYETu"]?.ToString() ?? "";
                        data["{{36}}"] = reader["QYEDen"]?.ToString() ?? "";
                        data["{{37}}"] = reader["TramYTeTu"]?.ToString() ?? "";
                        data["{{38}}"] = reader["TramYTeDen"]?.ToString() ?? "";
                        data["{{39}}"] = reader["TongTu"]?.ToString() ?? "";
                        data["{{40}}"] = reader["TongDen"]?.ToString() ?? "";
                    }
                }
                data["{{45}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "_3YDinh") ?? "";
                data["{{46}}"] = dataLayer6.LayThongTin("BaoDuongSuaChua_1");

                var cmd9 = new SQLiteCommand("SELECT sl,ty_le_hu_hong,nhe,vua,nang,huy,cong FROM suachua_tbkt WHERE User = @UserId", connection);
                cmd9.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);
                using (var reader = cmd9.ExecuteReader())
                {
                    int i = 241;
                    while (reader.Read())
                    {
                        data["{{" + i++ + "}}"] = reader["sl"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["ty_le_hu_hong"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["nhe"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["vua"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["nang"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["huy"]?.ToString() ?? "";
                        data["{{" + i++ + "}}"] = reader["cong"]?.ToString() ?? "";
                    }
                }
                var cmd10 = new SQLiteCommand("SELECT * FROM du_tinh_khoi_luong WHERE UserId = @UserID", connection);
                cmd10.Parameters.AddWithValue("@UserID", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd10.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{47}}"] = reader["KhoiLuongToanTran"]?.ToString();
                        data["{{48}}"] = reader["VCHCToanTran"]?.ToString();
                        data["{{49}}"] = reader["KhoiLuongGiaiDoanChuanBi"]?.ToString();
                        data["{{50}}"] = reader["VCHCChuanBi"]?.ToString();
                        data["{{51}}"] = reader["VCKTChuanBi"]?.ToString();
                        data["{{52}}"] = reader["KhoiLuongGiaiDoanChienDau"]?.ToString();
                        data["{{554}}"] = reader["VCHCChienDau"]?.ToString();
                        data["{{555}}"] = reader["VCKTChienDau"]?.ToString();

                    }
                }
                data["{{318}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai1") ?? "";
                data["{{319}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai2") ?? "";
                data["{{320}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai3") ?? "";
                data["{{321}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "vantai4") ?? "";


                var cmd11 = new SQLiteCommand(
                    "select vtbi_from,vtbi_to,vtle_from ,vtle_to ,danquan_from ,danquan_to ,xetho_count ,xetho_from,xetho_to,tongkha_from,tongkha_to,ketluan  FROM  candoivt WHERE UserId = @UserId",
                    connection
                );

                cmd11.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd11.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{53}}"] = reader["vtbi_from"]?.ToString() ?? "";
                        data["{{54}}"] = reader["vtbi_to"]?.ToString() ?? "";
                        data["{{55}}"] = reader["vtle_from"]?.ToString() ?? "";
                        data["{{56}}"] = reader["vtle_to"]?.ToString() ?? "";
                        data["{{57}}"] = reader["danquan_from"]?.ToString() ?? "";
                        data["{{58}}"] = reader["danquan_to"]?.ToString() ?? "";
                        data["{{59}}"] = reader["xetho_count"]?.ToString() ?? "";
                        data["{{60}}"] = reader["xetho_from"]?.ToString() ?? "";
                        data["{{61}}"] = reader["xetho_to"]?.ToString() ?? "";
                        data["{{62}}"] = reader["tongkha_from"]?.ToString() ?? "";
                        data["{{63}}"] = reader["tongkha_to"]?.ToString() ?? "";
                        data["{{64}}"] = reader["ketluan"]?.ToString() ?? "";
                    }
                }
                data["{{65}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "IX_4_YdinhVanChuyen") ?? "";
                data["{{66}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "X_DuKien") ?? "";
                data["{{67}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "X_BienPhap") ?? "";
                var cmd12 = new SQLiteCommand(
                    "SELECT chihuy_hckt, nguoithaythe\r\n           FROM thongtintepbai\r\n       WHERE User= @UserId",
                    connection
                );

                cmd12.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                using (var reader = cmd12.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data["{{68}}"] = reader["chihuy_hckt"]?.ToString() ?? "";
                        data["{{69}}"] = reader["nguoithaythe"]?.ToString() ?? "";
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
                        data["{{70}}"] = reader["lienlac1"]?.ToString() ?? "";
                        data["{{71}}"] = reader["lienlac2"]?.ToString() ?? "";
                        data["{{72}}"] = reader["moc1"]?.ToString() ?? "";
                        data["{{73}}"] = reader["moc2"]?.ToString() ?? "";
                    }
                }

                data["{{74}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "XI_Ketluan") ?? "";
                data["{{75}}"] = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "XI_Denghi") ?? "";

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