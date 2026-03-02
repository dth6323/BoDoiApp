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

namespace BoDoiApp.View.IVVuKhiKyThuat
{
    public partial class _2BienPhapBaoDam : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        private string content = string.Empty;
        public _2BienPhapBaoDam()
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

        private void button3_Click(object sender, EventArgs e)
        {
            if(content != string.Empty)
            {
                dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "BienPhapBaoDam2");
            }
            else
            {
                dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "BienPhapBaoDam2");
            }
        }

        private void _2BienPhapBaoDam_Load(object sender, EventArgs e)
        {
            content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapBaoDam2");
            if(content != string.Empty)
            {
                richTextBox1.Text = content;
            }
        }
        
    }
}
