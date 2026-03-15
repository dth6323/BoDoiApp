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
using BoDoiApp.Export;

namespace BoDoiApp
{
    public partial class Form1 : UserControl
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_xuatbaocaodukien_Click(object sender, EventArgs e)
        {
            var export = new ExportKeHoach();
            export.ExportWord();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var export = new WordExporter();
            export.ExportWord();        }

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
            NavigationService.Navigate(() => new FormBaoDamHauCan());
        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển. Vui lòng chờ cập nhật sau.");
        }
        private void btn_khbdhckt_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new FormKeHoach());

        }

        private void btn_kbdl_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new ThongTinTapBai());
        }
        
    }
}
