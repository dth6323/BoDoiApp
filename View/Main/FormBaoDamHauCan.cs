using BoDoiApp.View.Baovehaucankythuat;
using BoDoiApp.View.IXCongTacVanTai;
using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.VICongTacVanTai;
using BoDoiApp.View.VIIBaoDamQuanY;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
using BoDoiApp.View.XIHauCanKyThuat;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.Main
{
    public partial class FormBaoDamHauCan : UserControl
    {
        // ===== FONT DÙNG CHUNG =====
        private readonly Font titleFont = new Font("Times New Roman", 12, FontStyle.Bold);
        private readonly Font headerFont = new Font("Times New Roman", 12, FontStyle.Bold);
        private readonly Font btnFont = new Font("Times New Roman", 11);

        public FormBaoDamHauCan()
        {
            InitializeComponent();
            EnableDoubleBuffering();
        }

        private void EnableDoubleBuffering()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
        }

        // ===== TẠO BUTTON MENU =====
        private Button CreateMenuButton(string text, string tag, Color color)
        {
            Button btn = new Button
            {
                Text = text,
                Tag = tag,
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = color,
                Font = btnFont,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };

            btn.Click += MenuButton_Click;
            return btn;
        }

        // ===== EVENT MENU =====
        private void MenuButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Tag == null) return;

            switch (btn.Tag.ToString())
            {
                case "I_DANH_GIA":
                    NavigationService.Navigate(() => new TinhHinhTacDong());
                    break;

                case "II_NHIEM_VU":
                    NavigationService.Navigate(() => new NhiemVu());
                    break;

                case "III_TO_CHUC":
                    NavigationService.Navigate(() => new ToChucSuDungBoTri());
                    break;

                case "IV_VU_KHI":
                    NavigationService.Navigate(() => new BaoDamVuKhi());
                    break;

                case "V_VAT_CHAT":
                    NavigationService.Navigate(() => new DanVatChatVatTu());
                    break;

                case "VI_SINH_HOAT":
                    NavigationService.Navigate(() => new View.VIBaoDamSinhHoat._1BaoDamAnUong());
                    break;

                case "VII_QUAN_Y":
                    NavigationService.Navigate(() => new _1BaoDamQuanY());
                    break;

                case "VIII_BAO_DUONG":
                    NavigationService.Navigate(() => new _1BaoDuongSuaChua());
                    break;

                case "IX_VAN_TAI":
                    NavigationService.Navigate(() => new DuongVanT());
                    break;

                case "X_BAO_VE":
                    NavigationService.Navigate(() => new _1DukienTinhHuong());
                    break;

                case "XI_CHI_HUY":
                    NavigationService.Navigate(() => new _1ChiHuyHauCanKyThuat());
                    break;

                case "KET_LUAN":
                    NavigationService.Navigate(() => new _2KetLuanVaDeNghi());
                    break;
            }
        }
    }
}