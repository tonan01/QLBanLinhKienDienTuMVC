using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        //kết nối
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();

     
        public ActionResult ThanhToanDH()
        {
           
            var tk = Session["TaiKhoan"] as KhachHang;
            ThanhToan tt = new ThanhToan();
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            foreach (var m in lstGioHang)
            {
                //thanh toán thêm vào csdl
                if (tt.ThanhToanTien(tt.LayMaDH(tk.UserName), tk.UserName, m.iMaSP, m.iSoLuong) > 0)
                {
                    ViewBag.BT1 = "Thành Công";
                }
                else
                {
                    ViewBag.BT1 = "Thất Bại";
                }
                    
                    
            }
            Session["GioHang"] = null;
            return RedirectToAction("GioHang", "GioHang");
        }
        
    }
}