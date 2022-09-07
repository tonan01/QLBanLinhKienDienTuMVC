using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
       
        //tim kiem
        public ActionResult TimKiemPartial()
        {
            return View();
        }

        //hiển thị sản phẩm tìm kiếm
        public ActionResult TimKiemSanPham(FormCollection f)
        {
            TimKiem tk = new TimKiem();
            var lisSanPham = tk.getData(f["txtSearch"].ToString());
            //nếu không có sản phẩm nào
            if (lisSanPham.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào thuộc loại này";
            }
            return View(lisSanPham);
        }

    }
}