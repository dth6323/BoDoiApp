namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class baoDamVatChatHauCanKyThuat
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.loaiVatChat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quyDinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hienCo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boSung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Dự kiến khối lượng đạn, vật chất hậu cần, vật tư kỹ thuật";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.loaiVatChat,
            this.DVT,
            this.quyDinh,
            this.hienCo,
            this.boSung});
            this.dataGridView1.Location = new System.Drawing.Point(107, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(560, 286);
            this.dataGridView1.TabIndex = 1;
            // 
            // loaiVatChat
            // 
            this.loaiVatChat.HeaderText = "Loại vật chất";
            this.loaiVatChat.Name = "loaiVatChat";
            this.loaiVatChat.ReadOnly = true;
            // 
            // DVT
            // 
            this.DVT.HeaderText = "ĐVT";
            this.DVT.Name = "DVT";
            this.DVT.ReadOnly = true;
            // 
            // quyDinh
            // 
            this.quyDinh.HeaderText = "Quy Định";
            this.quyDinh.Name = "quyDinh";
            // 
            // hienCo
            // 
            this.hienCo.HeaderText = "Hiện có";
            this.hienCo.Name = "hienCo";
            // 
            // boSung
            // 
            this.boSung.HeaderText = "Bổ sung";
            this.boSung.Name = "boSung";
            // 
            // baoDamVatChatHauCanKyThuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "baoDamVatChatHauCanKyThuat";
            this.Text = "duKienKeHoachDamBaoHauCanKyThuat";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiVatChat;
        private System.Windows.Forms.DataGridViewTextBoxColumn DVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn quyDinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn hienCo;
        private System.Windows.Forms.DataGridViewTextBoxColumn boSung;
    }
}