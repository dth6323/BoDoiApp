using BoDoiApp.View.Baovehaucankythuat;
using BoDoiApp.View.IIIToChucSudung;
using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.VICongTacVanTai;
using BoDoiApp.View.VIIBaoDamQuanY;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
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

namespace BoDoiApp.View.Main
{
    public partial class FormKeHoach : UserControl
    {
        public FormKeHoach()
        {
            InitializeComponent();
        }

        private void FormKeHoach_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

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

            // ===== PANEL CENTER =====
            Panel pnlCenter = new Panel
            {
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(pnlCenter, 0, 1);

            // ===== KHUNG MENU =====
            Panel pnlBox = new Panel
            {
                Width = 850,
                Height = 420,
                BorderStyle = BorderStyle.FixedSingle
            };

            pnlBox.Anchor = AnchorStyles.None;
            pnlCenter.Controls.Add(pnlBox);
            pnlBox.Location = new Point(
                (pnlCenter.Width - pnlBox.Width) / 2,
                (pnlCenter.Height - pnlBox.Height) / 2
            );

            pnlCenter.Resize += (s, e2) =>
            {
                pnlBox.Location = new Point(
                    (pnlCenter.Width - pnlBox.Width) / 2,
                    (pnlCenter.Height - pnlBox.Height) / 2
                );
            };

            // ===== HEADER NHỎ =====
            Label lblHeader = new Label
            {
                Text = "Kế hoạch bảo đảm hậu cần - kỹ thuật",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.FromArgb(226, 239, 218),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlBox.Controls.Add(lblHeader);

            // ===== TABLE MENU =====
            TableLayoutPanel tblMenu = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 13
            };
            pnlBox.Controls.Add(tblMenu);
            tblMenu.BringToFront();

            string[] texts =
            {
        "I. Kết luận, đánh giá tình hình",
        "II. Nhiệm vụ",
        "III. Tổ chức, sử dụng lực lượng, bố trí hậu cần - kỹ thuật",
        "IV. Bảo đảm vũ khí trang bị kỹ thuật",
        "V. Bảo đảm đạn, vật chất hậu cần, vật tư kỹ thuật",
        "VI. Bảo đảm sinh hoạt",
        "VII. Bảo đảm quân y",
        "VIII. Bảo dưỡng, sửa chữa",
        "IX. Công tác vận tải",
        "X. Bảo vệ hậu cần - kỹ thuật",
        "XI. Chỉ huy hậu cần - kỹ thuật"
    };

            string[] tags =
{
    "I_DANH_GIA",
    "II_NHIEM_VU",
    "III_TO_CHUC",
    "IV_VU_KHI",
    "V_VAT_CHAT",
    "VI_SINH_HOAT",
    "VII_QUAN_Y",
    "VIII_BAO_DUONG",
    "IX_VAN_TAI",
    "X_BAO_VE",
    "XI_CHI_HUY"
};
            Color[] colors =
{
    Color.FromArgb(226,239,218),
    Color.FromArgb(221,235,247),
    Color.FromArgb(242,220,219),
    Color.FromArgb(217,225,242),
    Color.FromArgb(255,229,204),
    Color.FromArgb(226,239,218),
    Color.FromArgb(221,235,247),
    Color.FromArgb(242,220,219),
    Color.FromArgb(221,235,247),
    Color.FromArgb(217,225,242),
    Color.FromArgb(255,229,204),
};

            for (int i = 0; i < texts.Length; i++)
            {
                tblMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                Button btn = new Button
                {
                    Text = texts[i],
                    Dock = DockStyle.Fill,
                    BackColor = colors[i],
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Times New Roman", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Tag = tags[i]   // ⭐ THÊM DÒNG NÀY
                };

                btn.FlatAppearance.BorderSize = 1;
                btn.Click += MenuButton_Click;

                tblMenu.Controls.Add(btn, 0, i);
            }

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

            Button btnBack = new Button
            {
                Text = "Trở về",
                BackColor = Color.FromArgb(252, 213, 180),
                Anchor = AnchorStyles.Left
            };
            btnBack.Click += (s, e2) => NavigationService.Back();

            Button btnHome = new Button
            {
                Text = "Trang chủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            };

            Button btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(189, 215, 238),
                Anchor = AnchorStyles.Right
            };

            pnlBottom.Controls.Add(btnBack, 0, 0);
            pnlBottom.Controls.Add(btnHome, 1, 0);
            pnlBottom.Controls.Add(btnSave, 2, 0);

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
                    NavigationService.Navigate(new _1ToChucSudung());
                    break;

                case "IV_VU_KHI":
                    NavigationService.Navigate(new View.IVVuKhiKyThuat._1ChiTieu());
                    break;

                case "V_VAT_CHAT":
                    NavigationService.Navigate(new View.VVatChatHauCanKyThuat2._1ChiTieu());
                    break;

                case "VI_SINH_HOAT":
                    NavigationService.Navigate(new View.VIBaoDamSinhHoat._1BaoDamAnUong());
                    break;

                case "VII_QUAN_Y":
                    NavigationService.Navigate(new KeHoachBaoDamQuanY());
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
