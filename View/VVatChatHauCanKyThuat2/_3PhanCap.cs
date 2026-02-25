using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class _3PhanCap : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public _3PhanCap()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "TiepNhanBoXungV");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void _3PhanCap_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TiepNhanBoXungV");
            richTextBox1.Text = content;
        }
    }
}
