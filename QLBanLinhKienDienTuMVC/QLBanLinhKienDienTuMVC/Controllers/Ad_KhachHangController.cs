using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class Ad_KhachHangController : KiemTraDangNhapAdminController
    {

        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        NguoiDung nd = new NguoiDung();
        // GET: Ad_KhachHang
        public ActionResult DanhSachKhachHang()
        {
            var LstKhachHang = db.KhachHangs.Where(c => c.UserName != "Admin").ToList();
            return View(LstKhachHang);
        }
        //hiển thị chi tiết sản phẩm
        public ActionResult XemKhachHang(string UserName)
        {
            //lấy ds của sp trùng vs sản phẩm truyền vào
            KhachHang khachHang = db.KhachHangs.Single(s => s.UserName == UserName);
            //nếu không tìm thấy
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        
        //Thêm Khách Hàng
        [HttpGet]
        public ActionResult ThemKhachHang()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemKhachHang(KhachHang k, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var hoten = f["HoTen"];
            var tendn = f["UserName"];
            var matkhau = nd.PassMD5("123");//pass mặc định là 123
            var dienthoai = f["DienThoai"];
            var email = f["Email"];
            var diachi = f["DiaChi"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(hoten))//nếu hoten là rỗng
            {
                ViewData["Loi1"] = "Họ tên không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "UserName không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))//nếu là rỗng
            {
                ViewData["Loi3"] = "Điện thoại không dược bỏ trống";
            }

            KhachHang kh = db.KhachHangs.FirstOrDefault(c => c.UserName == tendn);
            if (kh != null)//trùng tên đăng nhập
            {
                ViewData["Loi4"] = "Tên đăng nhập đã tồn tại";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn)
                && !String.IsNullOrEmpty(dienthoai) && kh==null)
            {
                //gán gá trị cho đối tượng
                k.HoTen = hoten;
                k.UserName = tendn;
                k.MatKhau = matkhau;
                k.DienThoai = dienthoai;
                k.DiaChi = diachi;
                k.Email = email;

                //ghi xuống csdl
                db.KhachHangs.InsertOnSubmit(k);
                db.SubmitChanges();
                //trở lại trang đăng nhập
                return RedirectToAction("DanhSachKhachHang", "Ad_KhachHang");
            }
            return View();
        }


        //Update Khách Hàng
        [HttpGet]
        public ActionResult CapNhatKhachHang(string UserName)
        {
            //lấy ds của sp trùng vs sản phẩm truyền vào
            KhachHang khachHang = db.KhachHangs.Single(s => s.UserName == UserName);
            //nếu không tìm thấy
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        [HttpPost]
        public ActionResult CapNhatKhachHang(KhachHang k, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var hoten = f["HoTen"];
            var tendn = f["UserName"];
            var dienthoai = f["DienThoai"];
            var email = f["Email"];
            var diachi = f["DiaChi"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(hoten))//nếu hoten là rỗng
            {
                ViewData["Loi1"] = "Họ tên không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))//nếu là rỗng
            {
                ViewData["Loi3"] = "Điện thoại không dược bỏ trống";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(dienthoai))
            {
                //lấy ra khách hang
                KhachHang kh = db.KhachHangs.FirstOrDefault(c => c.UserName == tendn);
                //gán gá trị cho đối tượng
                kh.HoTen = hoten;
                kh.DienThoai = dienthoai;
                kh.DiaChi = diachi;
                kh.Email = email;

                //update xuống csdl
                db.KhachHangs.GetModifiedMembers(kh);
                db.SubmitChanges();
               
                return RedirectToAction("DanhSachKhachHang", "Ad_KhachHang");
            }
            return View();
        }

        //Xóa Khách Hàng
        public ActionResult XoaKhachHang(string UserName)
        {
            try//thành công
            {
                //Lấy khách hàng cần xóa
                KhachHang kh = db.KhachHangs.Single(c => c.UserName == UserName);

                //xóa khách hàng
                db.KhachHangs.DeleteOnSubmit(kh);
                db.SubmitChanges();

                return RedirectToAction("DanhSachKhachHang", "Ad_KhachHang");
            }
            catch//thất Bại
            {
                ViewBag.TBTrung = "Tài Khoản này đã có mua sản phẩm không Thể Xóa";
                return View();
            }
        }
    }
}