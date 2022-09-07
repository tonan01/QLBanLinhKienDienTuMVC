using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class Ad_LoaiController : KiemTraDangNhapAdminController
    {
        // GET: Ad_Loai
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
      
        //danh sach loại
        public ActionResult DanhSachLoai()
        {
            var LstLoai = db.Loais.OrderBy(c => c.MaLoai).ToList();
            return View(LstLoai);
        }
        //hiển thị chi tiết loại
        public ActionResult XemChiTietLoai(int MaLoai)
        {
            //lấy ds của sp trùng vs sản phẩm truyền vào
            Loai loai = db.Loais.Single(s => s.MaLoai == MaLoai);
            //nếu không tìm thấy
            if (loai == null)
            {
                ViewBag.Loai = "Không có loại này";
            }
            return View(loai);
        }


        //Thêm Loai
        [HttpGet]
        public ActionResult ThemLoai()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemLoai(Loai l, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var tenloai = f["TenLoai"];
            var note = f["NoTe"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(tenloai))
            {
                ViewData["Loi2"] = "Tên Loại không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(note))//nếu là rỗng
            {
                ViewData["Loi3"] = "Note không dược bỏ trống";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(tenloai)&& !String.IsNullOrEmpty(note))
            {
                //gán gá trị cho đối tượng
                l.TenLoai = tenloai;
                l.NoTe = note;

                //ghi xuống csdl
                db.Loais.InsertOnSubmit(l);
                db.SubmitChanges();
                //trở lại trang đăng nhập
                return RedirectToAction("DanhSachLoai", "Ad_Loai");
            }
            return View();
        }


        //Update Loai
        [HttpGet]
        public ActionResult CapNhatLoai(int MaLoai)
        {
            //lấy ds của sp trùng vs loai truyền vào
            Loai loai = db.Loais.Single(s => s.MaLoai == MaLoai);
            //nếu không tìm thấy
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        [HttpPost]
        public ActionResult CapNhatLoai(int MaLoai,Loai l, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var tenloai = f["TenLoai"];
            var note = f["NoTe"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(tenloai))
            {
                ViewData["Loi2"] = "Tên Loại không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(note))//nếu là rỗng
            {
                ViewData["Loi3"] = "Note không dược bỏ trống";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(tenloai) && !String.IsNullOrEmpty(note))
            {
                //lấy ra Loai
                Loai kh = db.Loais.FirstOrDefault(c => c.MaLoai == MaLoai);
                //gán gá trị cho đối tượng
                kh.TenLoai = tenloai;
                kh.NoTe = note;

                //update xuống csdl
                db.Loais.GetModifiedMembers(kh);
                db.SubmitChanges();

                return RedirectToAction("DanhSachLoai", "Ad_Loai");
            }
            return View();
        }

        //Xóa Loai
        public ActionResult XoaLoai(int MaLoai)
        {
            try//thành công
            {
                //Lấy Loai cần xóa
                Loai l = db.Loais.Single(c => c.MaLoai == MaLoai);

                //xóa Loai
                db.Loais.DeleteOnSubmit(l);
                db.SubmitChanges();

                return RedirectToAction("DanhSachLoai", "Ad_Loai");
            }
            catch//thất Bại
            {
                ViewBag.TBTrung = "Loại đã có sản phẩm này không Thể Xóa";
                return View();
            }
        }
    }
}