using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class LoaiController : Controller
    {
        // GET: Loai
        //kết nối
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        //hiển thị danh sách tên loại
        public ActionResult LoaiPartail()
        {
            //lấy ds tên loại theo thứ tự
            var LstLoai = db.Loais.OrderBy(l => l.TenLoai).ToList();
            return View(LstLoai);
        }

        //hiển thị sản phẩm theo loại
        public ActionResult SPTheoLoai(int MaLoai)
        {
            //Lấy Ds Sản phẩm trùng vs Mã loại
            var LstSP = db.SanPhams.Where(s => s.MaLoai == MaLoai).OrderBy(s => s.GiaBan).ToList();
            //nếu không có sản phẩm nào
            if(LstSP.Count==0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào thuộc loại này";
            }
            return View(LstSP);
        }
    }
}