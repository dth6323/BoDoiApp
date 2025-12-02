using BoDoiApp.form;
using BoDoiApp.View.DangNhap;
using BoDoiApp.View.KhaiBaoDuLieuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoDoiApp.View
{
    public static class FormMana
    {
        public static Form1 Formmain { get; set; }
        public static dn Dangnhap { get; set; }
        public static DangKy Dangky { get; set; }
        public static KhaiBaoDuLieu KhaiBaoDuLieu { get; set; }
        public static ThongTinTapBai ThongTinTapBai { get; set; }
        public static QuanSoChienDau QuanSoChienDau { get; set; }
        public static VatChatHienCoView VatChatHienCo { get; set; }
        public static BanDo BanDo { get; set; }
        public static QuyDinhDuTruTieuThuBoSungVatChat QuyDinhDuTruTieuThuBoSung { get; set; }
        public static void Init()
        {
            QuyDinhDuTruTieuThuBoSung = new QuyDinhDuTruTieuThuBoSungVatChat();
            Formmain = new Form1();
            Dangnhap = new dn();
            Dangky = new DangKy();
            KhaiBaoDuLieu = new KhaiBaoDuLieu();
            ThongTinTapBai = new ThongTinTapBai();
            QuanSoChienDau = new QuanSoChienDau();
            VatChatHienCo = new VatChatHienCoView();
            BanDo = new BanDo();
        }
    }
}
