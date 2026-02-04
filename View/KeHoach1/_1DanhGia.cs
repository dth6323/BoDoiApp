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

namespace BoDoiApp.View.KeHoach1
{
    public partial class _1DanhGia : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public _1DanhGia()
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
            var content = richTextBox1.Text;
            var dataLayer = new RichTextBoxData();
            if (dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "KeHoachDanhGia") == string.Empty)
            {
                dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, content, "KeHoachDanhGia");
                return;
            }
            dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, content, "KeHoachDanhGia");
        }

        private void _1DanhGia_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "KeHoachDanhGia");
            if (content == string.Empty)
            {
                return;
            }
            richTextBox1.Text = content;
        }
    }
}
