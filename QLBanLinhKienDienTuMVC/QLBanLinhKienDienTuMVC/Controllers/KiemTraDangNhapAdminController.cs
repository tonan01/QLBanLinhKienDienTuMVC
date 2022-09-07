using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanLinhKienDienTuMVC.Models;
namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class KiemTraDangNhapAdminController : Controller
    {
        // GET: KiemTraDangNhapAdmin
        public KiemTraDangNhapAdminController()
        {
            if(System.Web.HttpContext.Current.Session["TaiKhoan"]==null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/NguoiDung/DangNhap");
            }
        }
    }
}