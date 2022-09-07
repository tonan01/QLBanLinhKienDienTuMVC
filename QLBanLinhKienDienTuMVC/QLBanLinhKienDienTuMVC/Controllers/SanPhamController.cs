using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        //Kết nối 
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();

        //hiển thị ds sản phẩm
        public ActionResult SanPhamPartail()
        {
            //lấy tên sản phẩm
            var LstSanPham = db.SanPhams.OrderBy(s => s.TenSP).ToList();
            return View(LstSanPham);
        }

        //hiển thị chi tiết sản phẩm
        public ActionResult XemChiTiet(int MaSP)
        {
            //lấy ds của sp trùng vs sản phẩm truyền vào
            SanPham sanpham = db.SanPhams.Single(s => s.MaSP == MaSP);
            //nếu không tìm thấy
            if(sanpham==null)
            {
               return HttpNotFound();
            }
            return View(sanpham);
        }
    }
}