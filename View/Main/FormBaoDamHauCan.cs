using BoDoiApp.View.Baovehaucankythuat;
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
        public FormBaoDamHauCan()
        {
            InitializeComponent();
            this.Load += FormBaoDamHauCan_Load;
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

            // ===== FLOW MENU (CĂN GIỮA) =====
            FlowLayoutPanel pnlMenu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            pnlMain.Controls.Add(pnlMenu);

            // ===== HEADER (KHÔNG CLICK) =====
            Label lblHeader = new Label
            {
                Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật",
                Width = 820,
                Height = 35,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 10, 0, 10)
            };
            pnlMenu.Controls.Add(lblHeader);

            // ===== MENU BUTTONS =====
            pnlMenu.Controls.Add(CreateMenuButton(
                "I. Đánh giá tình hình tác động đến hậu cần - kỹ thuật",
                "I_DANH_GIA",
                Color.FromArgb(226, 239, 218)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "II. Nhiệm vụ",
                "II_NHIEM_VU",
                Color.FromArgb(221, 235, 247)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "III. Tổ chức, sử dụng lực lượng, bố trí hậu cần - kỹ thuật",
                "III_TO_CHUC",
                Color.FromArgb(242, 220, 219)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "IV. Bảo đảm vũ khí trang bị kỹ thuật",
                "IV_VU_KHI",
                Color.FromArgb(217, 225, 242)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "V. Bảo đảm đạn, vật chất hậu cần, vật tư kỹ thuật",
                "V_VAT_CHAT",
                Color.FromArgb(255, 229, 204)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "VI. Bảo đảm sinh hoạt",
                "VI_SINH_HOAT",
                Color.FromArgb(226, 239, 218)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "VII. Bảo đảm quân y",
                "VII_QUAN_Y",
                Color.FromArgb(222, 235, 247)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "VIII. Bảo dưỡng, sửa chữa",
                "VIII_BAO_DUONG",
                Color.FromArgb(242, 220, 219)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "IX. Công tác vận tải",
                "IX_VAN_TAI",
                Color.FromArgb(221, 235, 247)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "X. Bảo vệ hậu cần - kỹ thuật",
                "X_BAO_VE",
                Color.FromArgb(217, 225, 242)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "XI. Chỉ huy hậu cần - kỹ thuật",
                "XI_CHI_HUY",
                Color.FromArgb(255, 229, 204)));

            pnlMenu.Controls.Add(CreateMenuButton(
                "Kết luận, đề nghị",
                "KET_LUAN",
                Color.Gold));

            // ===== BOTTOM =====
            TableLayoutPanel pnlBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.Controls.Add(pnlBottom, 0, 2);

            Button btnBack = new Button { Text = "Trở về", Anchor = AnchorStyles.Left };
            btnBack.Click += (s, e2) => NavigationService.Back();

            Button btnHome = new Button
            {
                Text = "Trang chủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            };

            Button btnSave = new Button { Text = "Lưu", Anchor = AnchorStyles.Right };

            pnlBottom.Controls.Add(btnBack, 0, 0);
            pnlBottom.Controls.Add(btnHome, 1, 0);
            pnlBottom.Controls.Add(btnSave, 2, 0);
        }

        // ===== TẠO BUTTON MENU =====
        private Button CreateMenuButton(string text, string tag, Color color)
        {
            Button btn = new Button
            {
                Text = text,
                Tag = tag,
                Width = 820,
                Height = 30,
                BackColor = color,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 3, 0, 3)
            };

            btn.Click += MenuButton_Click;
            return btn;
        }

        // ===== EVENT CHUNG =====
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
                    NavigationService.Navigate(new View.VIBaoDamSinhHoat._1BaoDamAnUong());
                    break;

                case "VII_QUAN_Y":
                    NavigationService.Navigate(new _1BaoDamQuanY());
                    break;

                case "VIII_BAO_DUONG":
                    NavigationService.Navigate(new _1BaoDuongSuaChua());
                    break;

                case "IX_VAN_TAI":
                    NavigationService.Navigate(new _1DuongVanTai());
                    break;

                case "X_BAO_VE":
                    NavigationService.Navigate(new _1DukienTinhHuong());
                    break;

                case "XI_CHI_HUY":
                    NavigationService.Navigate(new _1ChiHuyHauCanKyThuat());
                    break;
            }
        }
    }
}
