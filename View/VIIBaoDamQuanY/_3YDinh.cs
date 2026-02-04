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

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    public partial class _3YDinh : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public _3YDinh()
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

        private void _3YDinh_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "_3YDinh");   
            if (content == string.Empty)
            {
                return;
            }
            richTextBox1.Text = content;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "_3YDinh") == string.Empty)
            {
                dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "_3YDinh");
                return;
            }
            dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "_3YDinh");
        }
    }
}
