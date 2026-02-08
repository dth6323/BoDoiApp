using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.VIIBaoDamQuanY;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.Main
{
    public partial class FormBaoDamHauCan : UserControl
    {
        public FormBaoDamHauCan()
        {
            InitializeComponent();
        }

        private void FormBaoDamHauCan_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            // ===== MAIN LAYOUT =====
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            this.Controls.Add(layout);

            // ===== TITLE =====
            Label lblTitle = new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            layout.Controls.Add(lblTitle, 0, 0);

            // ===== CONTENT =====
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            layout.Controls.Add(pnlMain, 0, 1);

            // ===== BUTTON LIST =====
            string[] titles =
            {
                "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật",
                "I. Đánh giá tình hình tác động đến hậu cần - kỹ thuật",
                "II. Nhiệm vụ",
                "III. Tổ chức, sử dụng lực lượng, bố trí hậu cần - kỹ thuật",
                "IV. Bảo đảm vũ khí trang bị kỹ thuật",
                "V. Bảo đảm đạn, vật chất hậu cần, vật tư kỹ thuật",
                "VI. Bảo đảm sinh hoạt",
                "VII. Bảo đảm quân y",
                "VIII. Bảo dưỡng, sửa chữa",
                "IX. Công tác vận tải",
                "X. Bảo vệ hậu cần - kỹ thuật",
                "XI. Chỉ huy hậu cần - kỹ thuật",
                "Kết luận, đề nghị"
            };

            Color[] colors =
            {
                Color.FromArgb(255, 242, 204),
                Color.FromArgb(226, 239, 218),
                Color.FromArgb(221, 235, 247),
                Color.FromArgb(242, 220, 219),
                Color.FromArgb(217, 225, 242),
                Color.FromArgb(255, 229, 204),
                Color.FromArgb(226, 239, 218),
                Color.FromArgb(222, 235, 247),
                Color.FromArgb(242, 220, 219),
                Color.FromArgb(221, 235, 247),
                Color.FromArgb(217, 225, 242),
                Color.FromArgb(255, 229, 204),
                Color.Gold
            };

            int top = 10;

            for (int i = 0; i < titles.Length; i++)
            {
                Button btn = new Button
                {
                    Text = titles[i],
                    Left = 20,
                    Top = top,
                    Width = 780,
                    Height = 28,
                    BackColor = colors[i],
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleLeft,
                    FlatStyle = FlatStyle.Flat
                };

                pnlMain.Controls.Add(btn);
                top += 30;
            }

            // ===== BOTTOM BUTTONS =====
            TableLayoutPanel pnlBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.Controls.Add(pnlBottom, 0, 2);

            Button btnBack = new Button
            {
                Text = "Trở về",
                Anchor = AnchorStyles.Left
            };
            btnBack.Click += (s, e2) => NavigationService.Back();

            Button btnHome = new Button
            {
                Text = "Trang chủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            };
            btnHome.Click += (s, e2) =>
            {
                // NavigationService.Navigate(new HomeView());
            };

            Button btnBottomSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.Right
            };

            pnlBottom.Controls.Add(btnBack, 0, 0);
            pnlBottom.Controls.Add(btnHome, 1, 0);
            pnlBottom.Controls.Add(btnBottomSave, 2, 0);
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Tag == null)
                return;

            switch (btn.Tag.ToString())
            {
                case "I_DANH_GIA":
                    NavigationService.Navigate(new TinhHinhTacDong());
                    break;

                case "II_NHIEM_VU":
                    NavigationService.Navigate(new TinhHinhTacDong());
                    break;

                case "III_TO_CHUC":
                    NavigationService.Navigate(new ToChucSuDungBoTri());
                    break;

                case "IV_VU_KHI":
                    NavigationService.Navigate(new BaoDamVuKhi());
                    break;

                case "V_VAT_CHAT":
                    NavigationService.Navigate(new DanVatChatVatTu());
                    break;

                case "VI_SINH_HOAT":
                    NavigationService.Navigate(
                        new View.VIBaoDamSinhHoat._1BaoDamAnUong()
                    );
                    break;

                case "VII_QUAN_Y":
                    NavigationService.Navigate(new _1BaoDamQuanY());
                    break;

                case "VIII_BAO_DUONG":
                    NavigationService.Navigate(new _BaoDuongSuaChua());
                    break;

                case "IX_VAN_TAI":
                    NavigationService.Navigate(new _CongTacVanTai());
                    break;

                case "X_BAO_VE":
                    NavigationService.Navigate(new _BaoVeHauCan());
                    break;

                case "XI_CHI_HUY":
                    NavigationService.Navigate(new _ChiHuyHauCan());
                    break;

                case "KET_LUAN":
                    NavigationService.Navigate(new _KetLuanDeNghi());
                    break;
            }
        }

    }

}
