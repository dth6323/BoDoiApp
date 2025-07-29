namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class VatChatHienCoView
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.td = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.lvc,
            this.dvt,
            this.td,
            this.gc});
            this.dataGridView1.Location = new System.Drawing.Point(154, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(550, 319);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Lưu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // stt
            // 
            this.stt.Frozen = true;
            this.stt.HeaderText = "Stt";
            this.stt.Name = "stt";
            this.stt.ReadOnly = true;
            this.stt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.stt.Width = 50;
            // 
            // lvc
            // 
            this.lvc.Frozen = true;
            this.lvc.HeaderText = "Loại vật chất";
            this.lvc.Name = "lvc";
            this.lvc.ReadOnly = true;
            this.lvc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dvt
            // 
            this.dvt.HeaderText = "DVT";
            this.dvt.Name = "dvt";
            this.dvt.ReadOnly = true;
            this.dvt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dvt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // td
            // 
            this.td.HeaderText = "Toan d";
            this.td.Name = "td";
            this.td.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gc
            // 
            this.gc.HeaderText = "Ghi chú";
            this.gc.Name = "gc";
            this.gc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VatChatHienCoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "VatChatHienCoView";
            this.Text = "VatChatHienCoView";
            this.Load += new System.EventHandler(this.VatChatHienCoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn lvc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvt;
        private System.Windows.Forms.DataGridViewTextBoxColumn td;
        private System.Windows.Forms.DataGridViewTextBoxColumn gc;
    }
}