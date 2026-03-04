using BoDoiApp.View;
using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.Main;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BoDoiApp
{
    public partial class Form1 : UserControl
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           NavigationService.Navigate(new KhaiBaoDuLieu());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CenterButton();
        }

        //private void CenterButton()
        //{
        //    // Assuming the FlowLayoutPanel is named 'flowLayoutPanel1'
        //    if (flowLayoutPanel1 != null)
        //    {
        //        flowLayoutPanel1.Left = (this.ClientSize.Width - flowLayoutPanel1.Width) / 2;
        //        flowLayoutPanel1.Top = (this.ClientSize.Height - flowLayoutPanel1.Height) / 2;
        //    }
        //}

        private void Form1_Resize(object sender, EventArgs e)
        {
            //CenterButton();
        }

        private void btn_dkkhbdhckt_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new FormBaoDamHauCan());
        }

        private void btn_khbdhckt_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new FormKeHoach());

        }

        private void btn_kbdl_Click(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new ThongTinTapBai());
            string template = @"D:\code\BODOIAPP\Resources\word\word1.docx";
            string output = @"D:\word2.docx";

            ExportFromTemplate(
                template,
                output,
                "noi dung 2",
                "Nội dung thay cho $2"
            );

            MessageBox.Show("Xuất file thành công!");
        }
        public void ExportFromTemplate(
    string templatePath,
    string outputPath,
    string danhGiaValue,
    string value2)
        {
            // 1. Copy file gốc sang file mới
            File.Copy(templatePath, outputPath, true);

            // 2. Mở file mới để chỉnh sửa
            using (WordprocessingDocument wordDoc =
                WordprocessingDocument.Open(outputPath, true))
            {
                var texts = wordDoc.MainDocumentPart
                                   .Document
                                   .Descendants<Text>();

                foreach (var text in texts)
                {
                    if (text.Text.Contains("{danhgia}"))
                        text.Text = text.Text.Replace("{danhgia}", danhGiaValue);

                    if (text.Text.Contains("$2"))
                        text.Text = text.Text.Replace("$2", value2);
                }

                wordDoc.MainDocumentPart.Document.Save();
            }
        }
    }
}
