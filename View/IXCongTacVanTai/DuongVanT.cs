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
        private int PART = 0;
        public DuongVanT(int part = 0)
        {
            PART = part;
            InitializeComponent();

        }
        private RichTextBoxData dataLayer = new RichTextBoxData();

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveVanTai();
            if (PART == 0)
            {
                NavigationService.Navigate(() => new DuTinhKhoiLuongVanChuyen());
            }
            else
            {
                NavigationService.Navigate(() => new KeHoach9_2());
            }

        }
        private void DuongVanT_Load(object sender, EventArgs e)
        {
            LoadVanTai(); // ← Thêm dòng này
        }

        private void LoadVanTai()
        {
            TextBox[] listText = { txt1, txt2, txt3, txt4 };

            for (int i = 0; i < listText.Length; i++)
            {
                string key = "vantai" + (i + 1);
                string content = dataLayer.LoadDataFromDatabase(
                    Constants.CURRENT_USER_ID_VALUE, key);
                listText[i].Text = content;
            }
        }

        private void SaveVanTai()
        {
            TextBox[] listText = { txt1, txt2, txt3, txt4 };

            for (int i = 0; i < listText.Length; i++)
            {
                string key = "vantai" + (i + 1);
                string value = listText[i].Text.Trim();

                dataLayer.SaveOrUpdate( // ← Dùng SaveOrUpdate thay vì tự check
                    Constants.CURRENT_USER_ID_VALUE, value, key);
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

    }
}
