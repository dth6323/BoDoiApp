namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class QuyDinhDuTruTieuThuBoSungVatChat
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qddc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcscd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttgcdb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttgdcd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TT,
            this.lvc,
            this.dvt,
            this.qddc,
            this.pc,
            this.pcscd,
            this.ttgcdb,
            this.ttgdcd});
            this.dataGridView1.Location = new System.Drawing.Point(2, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(845, 404);
            this.dataGridView1.TabIndex = 0;
            // 
            // TT
            // 
            this.TT.HeaderText = "TT";
            this.TT.Name = "TT";
            this.TT.ReadOnly = true;
            this.TT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lvc
            // 
            this.lvc.HeaderText = "Loại vật chất";
            this.lvc.Name = "lvc";
            this.lvc.ReadOnly = true;
            this.lvc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dvt
            // 
            this.dvt.HeaderText = "ĐVT";
            this.dvt.Name = "dvt";
            this.dvt.ReadOnly = true;
            this.dvt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // qddc
            // 
            this.qddc.HeaderText = "Quy định dự trữ";
            this.qddc.Name = "qddc";
            this.qddc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pc
            // 
            this.pc.HeaderText = "Phải có 04.00N";
            this.pc.Name = "pc";
            this.pc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pcscd
            // 
            this.pcscd.HeaderText = "Phải có SCĐ";
            this.pcscd.Name = "pcscd";
            this.pcscd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ttgcdb
            // 
            this.ttgcdb.HeaderText = "Tiêu thụ GCĐB";
            this.ttgcdb.Name = "ttgcdb";
            this.ttgcdb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ttgdcd
            // 
            this.ttgdcd.HeaderText = "Tiêu thụ GDCD";
            this.ttgdcd.Name = "ttgdcd";
            this.ttgdcd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(475, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // QuyDinhDuTruTieuThuBoSungVatChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "QuyDinhDuTruTieuThuBoSungVatChat";
            this.Text = "QuyDinhDuTruTieuThuBoSungVatChat";
            this.Load += new System.EventHandler(this.QuyDinhDuTruTieuThuBoSungVatChat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT;
        private System.Windows.Forms.DataGridViewTextBoxColumn lvc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvt;
        private System.Windows.Forms.DataGridViewTextBoxColumn qddc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcscd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttgcdb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttgdcd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}