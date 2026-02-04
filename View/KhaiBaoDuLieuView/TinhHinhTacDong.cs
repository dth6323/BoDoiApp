using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class TinhHinhTacDong : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public TinhHinhTacDong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void TinhHinhTacDong_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TinhHinhTacDong");
            if(content != string.Empty)
            {
                richTextBox1.Text = content;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TinhHinhTacDong") == string.Empty)
            {
                dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "TinhHinhTacDong");
                return;
            }
            dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "TinhHinhTacDong");
        }
    }
}
