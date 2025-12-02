using BoDoiApp.DataLayer;
using System.Xml.Linq;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class BanDo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 82);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(320, 368);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(522, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 450);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.comboBox1);
            this.flowLayoutPanel2.Controls.Add(this.comboBox2);
            this.flowLayoutPanel2.Controls.Add(this.checkBox1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(320, 82);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Giai đoạn 1",
            "Giai đoạn 2",
            "Giai đoạn 3",
            "Giai đoạn 4",
            "Giai đoạn 5",
            "Giai đoạn 6",
            "Giai đoạn 7"});
            this.comboBox2.Location = new System.Drawing.Point(136, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 28);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "Giai đoạn 1";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 24);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Quân địch";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // BanDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 450);
            this.Controls.Add(this.panel1);
            this.Name = "BanDo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BanDo";
            this.Load += new System.EventHandler(this.BanDo_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}