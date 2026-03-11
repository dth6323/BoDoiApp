using BoDoiApp.Resources;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.XIHauCanKyThuat
{
    public partial class _1ChiHuyHauCanKyThuat : UserControl
    {
        private float currentFontSize = 11f;

        string user = Constants.CURRENT_USER_ID_VALUE;

        TextBox txtChiHuy;
        TextBox txtNguoiThayThe;

        TextBox txtLienLac1;
        TextBox txtLienLac2;

        TextBox txtMoc1;
        TextBox txtMoc2;

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

            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };

            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));

            Controls.Add(root);

            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

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
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            Panel box = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            main.Controls.Add(box, 0, 1);

            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2
            };

            form.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));

            box.Controls.Add(form);

            AddLabel(form, "1. Chỉ huy hậu cần, kỹ thuật");

            txtChiHuy = AddTextBox(form, "Người chỉ huy:", true);
            txtNguoiThayThe = AddTextBox(form, "Người thay thế:", true);

            AddLabel(form, "2. Quy định thông tin liên lạc");

            txtLienLac1 = AddTextBox(form, "GĐCB:");
            txtLienLac2 = AddTextBox(form, "GĐCĐ:");

            AddLabel(form, "3. Quy định bảo đảm và mốc thời gian");

            txtMoc1 = AddTextBox(form, "GĐCB:");
            txtMoc2 = AddTextBox(form, "GĐCĐ:");

            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };

            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            root.Controls.Add(bottom, 0, 2);
            Button btnBack = new Button
            {
                Text = "← Back",
                Dock = DockStyle.Left,
                Width = 100
            };

            btnBack.Click += (s, e2) =>
            {
                NavigationService.Back();
            };

            Button btnHome = new Button
            {
                Text = "⌂ Home",
                Dock = DockStyle.None,
                Width = 100,
                Anchor = AnchorStyles.None
            };

            btnHome.Click += (s, e2) =>
            {

                NavigationService.Navigate(() => new Form1());
            };


            bottom.Controls.Add(btnBack, 0, 0);
            bottom.Controls.Add(btnHome, 1, 0);
            Button btnSave = new Button
            {
                Text = "Lưu",
                Dock = DockStyle.Right,
                Width = 100
            };

            btnSave.Click += (s, e2) => SaveData();

            bottom.Controls.Add(btnSave, 2, 0);

            bottom.Controls.Add(btnSave, 2, 0);
            GetChiHuyInfo();
            LoadSavedData();
        }

        private void AddLabel(TableLayoutPanel panel, string text)
        {
            int row = panel.RowCount++;

            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            panel.Controls.Add(new Label
            {
                Text = text,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 4)
            }, 0, row);
        }

        private TextBox AddTextBox(TableLayoutPanel panel, string label, bool readOnly = false)
        {
            int row = panel.RowCount++;

            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            panel.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Times New Roman", 11),
                AutoSize = true
            }, 0, row);

            TextBox txt = new TextBox
            {
                Width = 180,
                ReadOnly = readOnly
            };

            panel.Controls.Add(txt, 1, row);

            return txt;
        }

        void GetChiHuyInfo()
        {
            using (var conn = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();

                string sql = @"
                    SELECT chihuy_hckt, nguoithaythe
                    FROM thongtintepbai
                    WHERE User=@User
                    LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User", user);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtChiHuy.Text = rd["chihuy_hckt"]?.ToString();
                            txtNguoiThayThe.Text = rd["nguoithaythe"]?.ToString();
                        }
                    }
                }
            }
        }

        void LoadSavedData()
        {
            using (var conn = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();

                string sql = @"
                    SELECT lienlac1,lienlac2,moc1,moc2
                    FROM chhckt
                    WHERE User=@User
                    LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User", user);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtLienLac1.Text = rd["lienlac1"]?.ToString();
                            txtLienLac2.Text = rd["lienlac2"]?.ToString();
                            txtMoc1.Text = rd["moc1"]?.ToString();
                            txtMoc2.Text = rd["moc2"]?.ToString();
                        }
                    }
                }
            }
        }

        void SaveData()
        {
            using (var conn = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();

                string checkSql = "SELECT COUNT(*) FROM chhckt WHERE User=@User";

                using (var checkCmd = new SQLiteCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@User", user);

                    long count = (long)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        string insertSql = @"
                INSERT INTO chhckt
                (User,lienlac1,lienlac2,moc1,moc2)
                VALUES
                (@User,@l1,@l2,@m1,@m2)";

                        using (var cmd = new SQLiteCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@User", user);
                            cmd.Parameters.AddWithValue("@l1", txtLienLac1.Text);
                            cmd.Parameters.AddWithValue("@l2", txtLienLac2.Text);
                            cmd.Parameters.AddWithValue("@m1", txtMoc1.Text);
                            cmd.Parameters.AddWithValue("@m2", txtMoc2.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string updateSql = @"
                UPDATE chhckt
                SET
                    lienlac1=@l1,
                    lienlac2=@l2,
                    moc1=@m1,
                    moc2=@m2
                WHERE User=@User";

                        using (var cmd = new SQLiteCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@User", user);
                            cmd.Parameters.AddWithValue("@l1", txtLienLac1.Text);
                            cmd.Parameters.AddWithValue("@l2", txtLienLac2.Text);
                            cmd.Parameters.AddWithValue("@m1", txtMoc1.Text);
                            cmd.Parameters.AddWithValue("@m2", txtMoc2.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Đã lưu dữ liệu");
        }

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
                if (currentFontSize > 8)
                    currentFontSize--;

                UpdateFont(this);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void UpdateFont(Control c)
        {
            c.Font = new Font("Times New Roman", currentFontSize, c.Font.Style);

            foreach (Control child in c.Controls)
                UpdateFont(child);
        }
    }
}