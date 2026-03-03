using BoDoiApp.DataLayer;
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
using unvell.ReoGrid;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    public partial class KeHoachBaoDamQuanY : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book2.xlsx");

        private ReoGridControl reoGridControl1;
        public KeHoachBaoDamQuanY()
        {
            InitializeComponent();
        }

        private void KeHoachBaoDamQuanY_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            Controls.Add(root);

            // ===== TITLE =====
            root.Controls.Add(new Label
            {
                Text = "PHẦN SỬA CHỮA",
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(255, 242, 204),
                Font = new System.Drawing.Font("Times New Roman", 13, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            }, 0, 0);

            // ===== MAIN =====
            Panel main = new Panel { Dock = DockStyle.Fill };
            root.Controls.Add(main, 0, 1);

            // ===== REOGRID =====
            reoGridControl1 = new ReoGridControl
            {
                Dock = DockStyle.Fill
            };
            main.Controls.Add(reoGridControl1);

            LoadExcelAndData();

            // ===== BOTTOM =====
            // ===== BOTTOM PANEL =====
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };

            // Chia 4 cột đều nhau
            for (int i = 0; i < 4; i++)
            {
                bottom.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25));
            }

            root.Controls.Add(bottom, 0, 2);


            // ===== NÚT TRỞ VỀ =====
            Button btnBack = new Button
            {
                Text = "Trở về",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };

            btnBack.Click += (s, ev) =>
            {
                NavigationService.Back();
            };

            bottom.Controls.Add(btnBack, 0, 0);


            // ===== NÚT TRANG CHỦ =====
            Button btnHome = new Button
            {
                Text = "Trang chủ",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };

            btnHome.Click += (s, ev) =>
            {
                NavigationService.Navigate(new Form1());
            };

            bottom.Controls.Add(btnHome, 1, 0);


            // ===== NÚT LƯU =====
            Button btnSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };

            btnSave.Click += BtnSave_Click;

            bottom.Controls.Add(btnSave, 2, 0);


            // ===== PANEL PHẢI (CHO NÚT TIẾP) =====
            Panel rightPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            bottom.Controls.Add(rightPanel, 3, 0);


            // ===== NÚT TIẾP =====
            Button btnNext = new Button
            {
                Text = "Tiếp",
                Anchor = AnchorStyles.Right,
                AutoSize = true
            };

            btnNext.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _3CanDoiVaYdinhBaoDam());
            };

            rightPanel.Controls.Add(btnNext);
        }
        private void LoadExcelAndData()
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("Không tìm thấy file Excel");
                return;
            }

            reoGridControl1.Load(EXCEL_PATH);

            // ===== Chọn sheet sửa chữa =====
            reoGridControl1.CurrentWorksheet =
                reoGridControl1.Worksheets["BaoDamQuanY"];

            var ws = reoGridControl1.CurrentWorksheet;

            // ===== 1. Khóa toàn bộ sheet =====
            for (int row = 0; row < ws.RowCount; row++)
            {
                for (int col = 0; col < ws.ColumnCount; col++)
                {
                    ws.Cells[row, col].IsReadOnly = true;
                }
            }

            // ===== 2. Mở khóa D5-D12 =====
            for (int row = 4; row <= 11; row++) // 5 → 12 (index bắt đầu từ 0)
            {
                ws.Cells[row, 3].IsReadOnly = false; // Cột D (index 3)
            }

            // ===== 3. Mở khóa F5-F12 =====
            for (int row = 4; row <= 11; row++)
            {
                ws.Cells[row, 5].IsReadOnly = false; // Cột F (index 5)
            }

            // ===== 4. Mở khóa H5-H12 =====
            for (int row = 4; row <= 11; row++)
            {
                ws.Cells[row, 7].IsReadOnly = false; // Cột H (index 7)
            }

            // Ẩn sheet tab
            reoGridControl1.SheetTabVisible = false;
            ws.HideColumns(10, ws.ColumnCount - 10);

            // Ẩn dòng 15 trở đi
            ws.HideRows(12, ws.RowCount - 12);
            // Load dữ liệu DB
            BaoDamQuanYData.LoadAll(reoGridControl1);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            BaoDamQuanYData.SaveAll(reoGridControl1);
        }
    }
}
