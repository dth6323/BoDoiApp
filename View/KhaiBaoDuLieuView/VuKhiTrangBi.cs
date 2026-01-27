using System;
using System.Windows.Forms;
using System.IO;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class VuKhiTrangBi : Form
    {
        public VuKhiTrangBi()
        {
            InitializeComponent();
        }

        private void VuKhiTrangBi_Load(object sender, EventArgs e)
        {
            // Load the Excel file and select sheet 3 (index 2)
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDir, "Resources", "Sheet", "Book1.xlsx");

            reoGridControl1.Load(filePath);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[2];
            
            // Configure anchoring for responsive layout
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            reoGridControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            
            // Center the grid horizontally
            CenterControlHorizontally();
            
            // Handle resize events
            this.Resize += VuKhiTrangBi_Resize;
        }

        private void VuKhiTrangBi_Resize(object sender, EventArgs e)
        {
            CenterControlHorizontally();
        }

        private void CenterControlHorizontally()
        {
            int availableWidth = this.ClientSize.Width;
            int gridWidth = reoGridControl1.Width;
            int leftMargin = (availableWidth - gridWidth) / 2;
            
            reoGridControl1.Left = Math.Max(0, leftMargin);
        }

    }
}
