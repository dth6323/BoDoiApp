using BoDoiApp.form;
using BoDoiApp.View.DangNhap;
using BoDoiApp.View.KhaiBaoDuLieuView;

namespace BoDoiApp.View
{
    public static class FormMana
    {
        public static BaoDamVuKhi BaoDamVuKhi { get; set; }
        public static Form1 Formmain { get; set; }
        public static dn Dangnhap { get; set; }
        public static DangKy Dangky { get; set; }
        public static KhaiBaoDuLieu KhaiBaoDuLieu { get; set; }
        public static ThongTinTapBai ThongTinTapBai { get; set; }
        public static QuanSoChienDau QuanSoChienDau { get; set; }
        public static VatChatHienCoView VatChatHienCo { get; set; }
        public static ChuYeu ChuYeu { get; set; }
        public static ThuYeu ThuYeu { get; set; }
        public static QuyDinhDuTruTieuThuBoSungVatChat QuyDinhDuTruTieuThuBoSung { get; set; }
        public static VuKhiTrangBi VuKhiTrangBi { get; set; }
        public static void Init()
        {
            BaoDamVuKhi = new BaoDamVuKhi();
            ThuYeu = new ThuYeu();
            QuyDinhDuTruTieuThuBoSung = new QuyDinhDuTruTieuThuBoSungVatChat();
            Formmain = new Form1();
            Dangnhap = new dn();
            Dangky = new DangKy();
            KhaiBaoDuLieu = new KhaiBaoDuLieu();
            ThongTinTapBai = new ThongTinTapBai();
            QuanSoChienDau = new QuanSoChienDau();
            ChuYeu = new ChuYeu();
            VatChatHienCo = new VatChatHienCoView();
            VuKhiTrangBi = new VuKhiTrangBi();
        }
    }
}
