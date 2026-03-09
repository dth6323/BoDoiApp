using BoDoiApp.DataLayer;
using BoDoiApp.DataLayer.KhaiBao;
using BoDoiApp.form;
using BoDoiApp.View.Baovehaucankythuat;
using BoDoiApp.View.IIIToChucSudung;
using BoDoiApp.View.IVVuKhiKyThuat;
using BoDoiApp.View.IXCongTacVanTai;
using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.Main;
using BoDoiApp.View.TinhHinhDonVi;
using BoDoiApp.View.VIBaoDamSinhHoat;
using BoDoiApp.View.VICongTacVanTai;
using BoDoiApp.View.VIIBaoDamQuanY;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
using BoDoiApp.View.VVatChatHauCanKyThuat2;
using BoDoiApp.View.XIHauCanKyThuat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            NavigationService.Init(this);
            NavigationService.Navigate(() => new VIIIBaoDuongSuaChua.KeHoachSuaChua());
        }

        public void ShowView(UserControl view)
        {
            // Dispose tất cả control cũ trước khi clear
            foreach (Control old in panel1.Controls)
                old.Dispose();

            panel1.Controls.Clear();

            view.Dock = DockStyle.Fill;
            panel1.Controls.Add(view);
        }

        private void panel1_Resize(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void MainForm_Load(object sender, EventArgs e) { }
    }
}