using BoDoiApp.DataLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ThongTinTapBai : UserControl
    {
        private Panel panelBottom;
        private Button btn_trove;
        private Button btn_trangchu;
        private Button btn_tieptheo;
        ThongTinTapBaiData thongTinTapBai = new ThongTinTapBaiData();
        bool isAddingNew = true;
        int currentId = 0;

        public ThongTinTapBai()
        {
            InitializeNavigationButtons();   // 👈 thêm dòng này
            InitializeComponent();
            this.Load += ThongTinTapBai_Load;
        }

        private void ThongTinTapBai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt = thongTinTapBai.LayThongTin();

            if (dt != null && dt.Rows.Count > 0)
            {
                isAddingNew = false;
                DataRow row = dt.Rows[0];

                currentId = Convert.ToInt32(row["id"]);

                txt_tenvankien.Text = row["thongtintapbai"]?.ToString() ?? "";
                txt_vtch.Text = row["vitrichihuy"]?.ToString() ?? "";
                txt_thoigian.Text = row["thoigian"]?.ToString() ?? "";

                txt_m1.Text = row["manh1"]?.ToString() ?? "";
                txt_m2.Text = row["manh2"]?.ToString() ?? "";
                txt_m3.Text = row["manh3"]?.ToString() ?? "";
                txt_m4.Text = row["manh4"]?.ToString() ?? "";

                txt_tyle.Text = row["tyle"]?.ToString() ?? "";
                txt_nam.Text = row["nam"]?.ToString() ?? "";
                txt_chhckt.Text = row["chihuyhckt"]?.ToString() ?? "";
                txt_nguoithaythe.Text = row["nguoithaythe"]?.ToString() ?? "";
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (FormMana.KhaiBaoDuLieu == null)
            {
                MessageBox.Show("Form KhaiBaoDuLieu đang NULL");
                return;
            }

            FormMana.KhaiBaoDuLieu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string thongTinTapBai = txt_tenvankien.Text;
            string viTriChiHuy = txt_vtch.Text;
            string thoiGian = txt_thoigian.Text;

            string manh1 = txt_m1.Text;
            string manh2 = txt_m2.Text;
            string manh3 = txt_m3.Text;
            string manh4 = txt_m4.Text;

            string tyle = txt_tyle.Text;
            string nam = txt_nam.Text;
            string chiHuyHCKT = txt_chhckt.Text;
            string nguoiThayThe = txt_nguoithaythe.Text;

            bool result;

            if (isAddingNew)
            {
                result = thongTinTapBai.ThemThongTin(
                    thongTinTapBaiText, viTriChiHuy, thoiGian,
                    manh1, manh2, manh3, manh4,
                    tyle, nam, chiHuyHCKT, nguoiThayThe
                );

                if (result)
                {
                    MessageBox.Show("Thêm thông tin thành công!", "Thành công");
                    isAddingNew = false;
                }
            }
            else
            {
                result = thongTinTapBai.CapNhatThongTin(
                    thongTinTapBaiText, viTriChiHuy, thoiGian,
                    manh1, manh2, manh3, manh4,
                    tyle, nam, chiHuyHCKT, nguoiThayThe
                );

                if (result)
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");
            }
        }
        private void InitializeNavigationButtons()
        {
            // ===== Panel dưới đáy =====
            panelBottom = new Panel();
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 60;
            panelBottom.BackColor = Color.FromArgb(230, 230, 230);

            // ===== Nút Trở về =====
            btn_trove = new Button();
            btn_trove.Text = "← Trở về";
            btn_trove.Width = 130;
            btn_trove.Height = 35;
            btn_trove.Left = 20;
            btn_trove.Top = 12;
            btn_trove.FlatStyle = FlatStyle.Flat;
            btn_trove.Click += btn_trove_Click;

            // ===== Nút Trang chủ =====
            btn_trangchu = new Button();
            btn_trangchu.Text = "🏠 Trang chủ";
            btn_trangchu.Width = 130;
            btn_trangchu.Height = 35;
            btn_trangchu.FlatStyle = FlatStyle.Flat;
            btn_trangchu.Top = 12;

            // căn giữa
            btn_trangchu.Left = (this.Width / 2) - (btn_trangchu.Width / 2);
            btn_trangchu.Anchor = AnchorStyles.Top;
            btn_trangchu.Click += btn_trangchu_Click;

            // ===== Nút Tiếp theo =====
            btn_tieptheo = new Button();
            btn_tieptheo.Text = "Tiếp theo →";
            btn_tieptheo.Width = 130;
            btn_tieptheo.Height = 35;
            btn_tieptheo.FlatStyle = FlatStyle.Flat;
            btn_tieptheo.Top = 12;
            btn_tieptheo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_tieptheo.Left = this.Width - btn_tieptheo.Width - 20;
            btn_tieptheo.Click += btn_tieptheo_Click;

            // Resize để luôn căn đúng khi form thay đổi kích thước
            this.Resize += (s, e) =>
            {
                btn_trangchu.Left = (this.Width / 2) - (btn_trangchu.Width / 2);
                btn_tieptheo.Left = this.Width - btn_tieptheo.Width - 20;
            };

            // Add vào panel
            panelBottom.Controls.Add(btn_trove);
            panelBottom.Controls.Add(btn_trangchu);
            panelBottom.Controls.Add(btn_tieptheo);

            // Add panel vào UserControl
            this.Controls.Add(panelBottom);
        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            // Vì là UserControl nên không dùng Hide()
            // Gọi event để form cha xử lý
            OnCloseRequested();
        }

        // Event để form cha bắt
        public event EventHandler CloseRequested;

        protected virtual void OnCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
        private void btn_trove_Click(object sender, EventArgs e)
        {
            //BackRequested?.Invoke(this, EventArgs.Empty);
            NavigationService.Back();
        }

        private void btn_trangchu_Click(object sender, EventArgs e)
        {
           // HomeRequested?.Invoke(this, EventArgs.Empty);

        }

        private void btn_tieptheo_Click(object sender, EventArgs e)
        {
            // NextRequested?.Invoke(this, EventArgs.Empty);
            NavigationService.Navigate(new BienChe());
        }
    }
}