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

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class DuongVanT : UserControl
    {
        public DuongVanT()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;

        }
        private RichTextBoxData dataLayer = new RichTextBoxData();

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveVanTai();
            NavigationService.Navigate(() => new DuTinhKhoiLuongVanChuyen());

        }
        private void SaveVanTai()
        {
            TextBox[] listText =
            {
        txt1,
        txt2,
        txt3,
        txt4
    };

            for (int i = 0; i < listText.Length; i++)
            {
                string key = "vantai" + (i + 1);
                string value = listText[i].Text.Trim();

                var content = dataLayer.LoadDataFromDatabase(
                    Constants.CURRENT_USER_ID_VALUE, key);

                if (string.IsNullOrEmpty(content))
                    dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, value, key);
                else
                    dataLayer.UpdateData(Constants.CURRENT_USER_ID_VALUE, value, key);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void DuongVanT_Load(object sender, EventArgs e)
        {

        }
    }
}
