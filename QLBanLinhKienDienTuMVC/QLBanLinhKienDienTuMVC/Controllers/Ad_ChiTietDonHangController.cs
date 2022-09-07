using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class Ad_ChiTietDonHangController : KiemTraDangNhapAdminController
    {
        // GET: Ad_ChiTietDonHang
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        //danh sach ChiTietDonHang
        public ActionResult DanhSachChiTietDonHang(int MaDonHang)
        {
            var LstChiTietDonHang = db.ChiTietDonHangs.Where(c => c.MaDonHang==MaDonHang).ToList();
            if(LstChiTietDonHang.Count==0)
            {
                HttpNotFound();
            }
            return View(LstChiTietDonHang);
        }
    }
}