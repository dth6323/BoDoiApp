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
    public partial class TiepNhanBoSung : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public TiepNhanBoSung()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var content = richTextBox1.Text;
            var dataLayer = new RichTextBoxData();
            dataLayer.SaveOrUpdate(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "TiepNhanBoSung");

        }

        private void TiepNhanBoSung_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "TiepNhanBoSung");
            if(content == string.Empty)
            {
                return;
            }
            richTextBox1.Text = content;
        }

    }
}
