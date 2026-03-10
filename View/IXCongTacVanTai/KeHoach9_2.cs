using BoDoiApp.DataLayer;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
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
using unvell.ReoGrid;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class KeHoach9_2 : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "test3.xlsx");

        private ReoGridControl reoGridControl1;
        public KeHoach9_2()
        {
            InitializeComponent();
        }

        private void KeHoach9_2_Load(object sender, EventArgs e)
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
                Anchor = AnchorStyles.None,
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
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            btnHome.Click += (s, ev) =>
            {
                NavigationService.Navigate(() => new Form1());
            };

            bottom.Controls.Add(btnHome, 1, 0);


            // ===== NÚT LƯU =====
            Button btnSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            btnSave.Click += BtnSave_Click;

            bottom.Controls.Add(btnSave, 2, 0);


            // ===== PANEL PHẢI (CHO NÚT TIẾP) =====


            // ===== NÚT TIẾP =====
            Button btnTiep = new Button();
            btnTiep.Text = "Tiếp";
            btnTiep.Size = new Size(100, 30);
            btnTiep.Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 50);
            btnTiep.Anchor = AnchorStyles.None;

            btnTiep.Click += (s, e2) =>
            {
                NavigationService.Navigate(() => new KeHoach93());
            };

            bottom.Controls.Add(btnTiep,3,0);
        }

        // =============================
        // LOAD EXCEL + LOAD DATA
        // =============================
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
                reoGridControl1.Worksheets["KhoiLuongVanTai"];

            var ws = reoGridControl1.CurrentWorksheet;
            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    ws.Cells[r, c].IsReadOnly = true;
                }
            }

            // =============================
            // 2. UNLOCK B→L (8→10)
            // =============================
            for (int r = 7; r <= 9; r++)       // dòng 8-10
            {
                for (int c = 1; c <= 11; c++)  // cột B-L
                {
                    ws.Cells[r, c].IsReadOnly = false;
                }
            }

            // =============================
            // 3. UNLOCK B→L (12→14)
            // =============================
            for (int r = 11; r <= 13; r++)     // dòng 12-14
            {
                for (int c = 1; c <= 11; c++)  // cột B-L
                {
                    ws.Cells[r, c].IsReadOnly = false;
                }
            }

            // =============================
            // 4. UNLOCK M6→M14
            // =============================
            for (int r = 5; r <= 13; r++)      // dòng 6-14
            {
                ws.Cells[r, 12].IsReadOnly = false; // cột M
            }
            ws.HideColumns(13, ws.ColumnCount - 13);

            // Ẩn dòng 15 trở đi
            ws.HideRows(14, ws.RowCount - 14);

            // Ẩn sheet tab
            reoGridControl1.SheetTabVisible = false;
            // ===== Load dữ liệu DB =====
            KhoiLuongVanTaiData.LoadAll(reoGridControl1);
        }

        // =============================
        // SAVE BUTTON
        // =============================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            KhoiLuongVanTaiData.SaveAll(reoGridControl1);
        }
    }
}
