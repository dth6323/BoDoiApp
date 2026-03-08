using BoDoiApp.DataLayer;
using BoDoiApp.DataLayer.KhaiBao;
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

namespace BoDoiApp.View.TinhHinhDonVi
{
    public partial class TinhHinhVc : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "TinhHinhDonVi.xlsx");

        private ReoGridControl reoGridControl1;
        public TinhHinhVc()
        {
            InitializeComponent();
        }

        private void TinhHinhVc_Load(object sender, EventArgs e)
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
                Text = "Tình hình vật chất",
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
            Button btnNext = new Button
            {
                Text = "Tiếp",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };


            btnNext.Click += (s, e2) =>
            {
                NavigationService.Navigate(() => new ChiLenhHKT1());
            };
            bottom.Controls.Add(btnNext, 3, 0);
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
                reoGridControl1.Worksheets["TinhHinhVc"];

            var ws = reoGridControl1.CurrentWorksheet;

            // ===== 1. Khóa toàn bộ sheet =====
            for (int row = 0; row < ws.RowCount; row++)
            {
                for (int col = 0; col < ws.ColumnCount; col++)
                {
                    ws.Cells[row, col].IsReadOnly = true;
                }
            }

            // ===== 2. Danh sách dòng được phép sửa =====
            List<int> editableRows = new List<int>();

            editableRows.AddRange(Enumerable.Range(3, 10));  // 4-13
            editableRows.AddRange(Enumerable.Range(15, 5));  // 16-19
            editableRows.AddRange(Enumerable.Range(21, 4));  // 22-25
            editableRows.Add(26); // row 27
            editableRows.Add(28);

            // ===== 3. Mở khóa cột D->G =====
            foreach (var row in editableRows)
            {
                for (int col = 3; col <= 6; col++) // D-G
                {
                    ws.Cells[row, col].IsReadOnly = false;
                    Console.WriteLine($"Mở khóa cell ({row}, {col})");
                }
            }

            // Ẩn sheet tab
            reoGridControl1.SheetTabVisible = false;
            ws.HideColumns(7, ws.ColumnCount - 7);

            // Ẩn dòng 15 trở đi
            ws.HideRows(29, ws.RowCount - 29);
            // Load dữ liệu DB
            TinhHinhVcData.LoadAll(reoGridControl1);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            TinhHinhVcData.SaveAll(reoGridControl1);
        }
    }
}
