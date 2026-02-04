using BoDoiApp.View.Baovehaucankythuat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.XIHauCanKyThuat
{
    public partial class _1ChiHuyHauCanKyThuat : UserControl
    {
        private float currentFontSize = 11f;

        public _1ChiHuyHauCanKyThuat()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _1ChiHuyHauCanKyThuat_Load;
        }

        private void _1ChiHuyHauCanKyThuat_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            Controls.Add(root);

            // ===== TITLE =====
            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            // ===== MAIN =====
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                Padding = new Padding(10)
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(main, 0, 1);

            main.Controls.Add(new Label
            {
                Text = "XI. Chỉ huy hậu cần – kỹ thuật",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(180, 198, 231),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0)
            }, 0, 0);

            // ===== CONTENT =====
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            main.Controls.Add(content, 0, 1);


            // ===== BOX =====
            Panel box = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            content.Controls.Add(box, 1, 0);

            // ===== FORM CONTENT =====
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            box.Controls.Add(form);

            AddRow(form, "1. Chỉ huy hậu cần, kỹ thuật");
            AddRow(form, "Người chỉ huy:", true);
            AddRow(form, "Người thay thế:", true);

            AddRow(form, "2. Quy định thông tin liên lạc");
            AddRow(form, "GĐĐ:", true);
            AddRow(form, "GĐĐ:", true);

            AddRow(form, "3. Quy định bảo đảm và mốc thời gian");
            AddRow(form, "GĐĐ:", true);
            AddRow(form, "GĐĐ:", true);

            // ===== NEXT =====
            Button btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Red
            };
            btnNext.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _2KetLuanVaDeNghi());
            };
            content.Controls.Add(btnNext, 2, 0);

            // ===== BOTTOM =====
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            root.Controls.Add(bottom, 0, 2);

            bottom.Controls.Add(MakeBottomButton("Trở về", Color.FromArgb(252, 213, 180), DockStyle.Left), 0, 0);
            bottom.Controls.Add(MakeBottomButton("Trang\nchủ", Color.Yellow, DockStyle.Fill), 1, 0);
            bottom.Controls.Add(MakeBottomButton("Lưu", Color.FromArgb(189, 215, 238), DockStyle.Right), 2, 0);
        }

        private void AddRow(TableLayoutPanel panel, string text, bool input = false)
        {
            int row = panel.RowCount++;
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            panel.Controls.Add(new Label
            {
                Text = text,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 5, 5, 5)
            }, 0, row);

            if (input)
            {
                panel.Controls.Add(new TextBox
                {
                    Width = 180
                }, 1, row);
            }
        }

        private Button MakeBottomButton(string text, Color color, DockStyle dock)
        {
            return new Button
            {
                Text = text,
                BackColor = color,
                Dock = dock,
                Width = 100,
                Height = 40
            };
        }

        // ===== ZOOM =====
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                currentFontSize++;
                UpdateFont(this);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Subtract))
            {
                if (currentFontSize > 8) currentFontSize--;
                UpdateFont(this);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateFont(Control c)
        {
            c.Font = new Font("Times New Roman", currentFontSize, c.Font.Style);
            foreach (Control child in c.Controls)
                UpdateFont(child);
        }
    }
}
