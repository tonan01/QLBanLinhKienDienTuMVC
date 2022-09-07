using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class GioHang
    {
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        public int iMaSP { set; get; }

        public string sTenSP { set; get; }

        public string sAnh { set; get; }

        public double dDonGia { set; get; }

        public int iSoLuong { set; get; }
        //tính thành tiên
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //giỏ hàng
        public GioHang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sp = db.SanPhams.Single(c => c.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sAnh = sp.Anh;
            dDonGia = double.Parse(sp.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}