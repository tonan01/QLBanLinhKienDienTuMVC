using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class Ad_SanPhamController : KiemTraDangNhapAdminController
    {
        // GET: Ad_SanPham
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();

        //danh sach SanPham
        public ActionResult DanhSachSanPham()
        {
            var LstSanPham = db.SanPhams.OrderBy(c => c.MaSP).ToList();
            return View(LstSanPham);
        }
        //hiển thị chi tiết loại
        public ActionResult XemChiTietSanPham(int MaSP)
        {
            //lấy ds của sp trùng vs sản phẩm truyền vào
            SanPham sanPham = db.SanPhams.Single(s => s.MaSP == MaSP);
            //nếu không tìm thấy
            if (sanPham == null)
            {
                ViewBag.Loai = "Không có sản phẩm này";
            }
            return View(sanPham);
        }


        //Thêm Loai
        [HttpGet]
        public ActionResult ThemSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSanPham(SanPham k, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var tensanpham = f["TenSP"];
            var giaban = f["GiaBan"];
            var mota = f["MoTa"];
            var anh = f["Anh"];
            var slton = f["SLTon"];
            var maloai = f["MaLoai"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(tensanpham))
            {
                ViewData["Loi1"] = "Tên sản phẩm không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(giaban))//nếu là rỗng
            {
                ViewData["Loi2"] = "giá bán không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(anh))
            {
                ViewData["Loi3"] = "chưa chọn ảnh";
            }
            if (String.IsNullOrEmpty(slton))//nếu là rỗng
            {
                ViewData["Loi4"] = "Số lượng không dược bỏ trống";
            }
            if (!String.IsNullOrEmpty(giaban) && int.Parse(giaban)<=0)
            {
                ViewData["Loi5"] = "Giá Bán Phải lớn hơn 0";
            }
            if (!String.IsNullOrEmpty(slton) && int.Parse(slton) <= 0)
            {
                ViewData["Loi6"] = "Số lượng tồn Phải lớn hơn 0";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(tensanpham) && !String.IsNullOrEmpty(giaban)
                && !String.IsNullOrEmpty(anh) && !String.IsNullOrEmpty(slton)
                && int.Parse(giaban) > 0 && int.Parse(slton) > 0)
            {
                //gán gá trị cho đối tượng
                k.TenSP = tensanpham;
                k.GiaBan = int.Parse(giaban);
                k.MoTa = mota;
                k.Anh = anh;
                k.SLTon = int.Parse(slton);
                k.MaLoai = int.Parse(maloai);

                //ghi xuống csdl
                db.SanPhams.InsertOnSubmit(k);
                db.SubmitChanges();
                //trở lại trang đăng nhập
                return RedirectToAction("DanhSachSanPham", "Ad_SanPham");
            }
            return View();
        }

        //comboboxloai
        public ActionResult DanhSachLoaiCBB()
        {
            var LstLoai = db.Loais.OrderBy(c => c.MaLoai).ToList();
            return View(LstLoai);
        }

        //Update Loai
        [HttpGet]
        public ActionResult CapNhatSanPham(int MaSP)
        {
            //lấy ds của sp trùng vs loai truyền vào
            SanPham sanPham = db.SanPhams.Single(s => s.MaSP == MaSP);
            //nếu không tìm thấy
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        public ActionResult CapNhatSanPham(int MaSP, SanPham kh, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var tensanpham = f["TenSP"];
            var giaban = f["GiaBan"];
            var mota = f["MoTa"];
            var anh = f["Anh"];
            var slton = f["SLTon"];
            var maloai = f["MaLoai"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(tensanpham))
            {
                ViewData["Loi1"] = "Tên sản phẩm không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(giaban))//nếu là rỗng
            {
                ViewData["Loi2"] = "giá bán không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(anh))
            {
                ViewData["Loi3"] = "chưa chọn ảnh";
            }
            if (String.IsNullOrEmpty(slton))//nếu là rỗng
            {
                ViewData["Loi4"] = "Số lượng không dược bỏ trống";
            }
            if (int.Parse(giaban) <= 0)
            {
                ViewData["Loi5"] = "Giá Bán Phải lớn hơn 0";
            }
            if (int.Parse(slton) <= 0)
            {
                ViewData["Loi6"] = "Số lượng tồn Phải lớn hơn 0";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(tensanpham) && !String.IsNullOrEmpty(giaban)
                && !String.IsNullOrEmpty(anh) && !String.IsNullOrEmpty(slton)
                && int.Parse(giaban) > 0 && int.Parse(slton) > 0)
            {
                SanPham k = db.SanPhams.FirstOrDefault(c => c.MaSP == MaSP);
                //gán gá trị cho đối tượng
                k.TenSP = tensanpham;
                k.GiaBan = int.Parse(giaban);
                k.MoTa = mota;
                k.Anh = anh;
                k.SLTon = int.Parse(slton);
                k.MaLoai = int.Parse(maloai);

                //ghi xuống csdl
                db.SanPhams.GetModifiedMembers(k);
                db.SubmitChanges();
                //trở lại trang đăng nhập
                return RedirectToAction("DanhSachSanPham", "Ad_SanPham");
            }
            return View();
        }

        //Xóa Loai
        public ActionResult XoaSanPham(int MaSP)
        {
            try//thành công
            {
                //Lấy sanpham cần xóa
                SanPham sp = db.SanPhams.Single(c => c.MaSP == MaSP);

                //xóa Loai
                db.SanPhams.DeleteOnSubmit(sp);
                db.SubmitChanges();

                return RedirectToAction("DanhSachSanPham", "Ad_SanPham");
            }
            catch//thất Bại
            {
                ViewBag.TBTrung = "sản phẩm này đã có trong chi tiết hóa đơn ko thể xóa";
                return View();
            }
        }
    }
}