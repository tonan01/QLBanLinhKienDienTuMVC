using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        //kết nối
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        NguoiDung nd = new NguoiDung();
        //đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang k, FormCollection f)
        {
            //các giá trị người dùng nhập vào
            var hoten = f["HoTen"];
            var tendn = f["UserName"];
            var matkhau = nd.PassMD5(f["MatKhau"]);
            var rematkhau = nd.PassMD5(f["ReMatkhau"]);
            var dienthoai = f["DienThoai"];
            var email = f["Email"];
            var diachi = f["DiaChi"];

            //dùng để kiểm tra rỗng
            var ktmatkhau = f["MatKhau"];
            var ktrematkhau = f["ReMatkhau"];
            //kiểm tra lỗi
            if (String.IsNullOrEmpty(hoten))//nếu hoten là rỗng
            {
                ViewData["Loi1"] = "Họ tên không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên đăng nhập không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(ktmatkhau))//nếu là rỗng
            {
                ViewData["Loi3"] = "Mật khẩu không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(ktrematkhau))//nếu là rỗng
            {
                ViewData["Loi4"] = "Mật khẩu không dược bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))//nếu là rỗng
            {
                ViewData["Loi5"] = "Điện thoại không dược bỏ trống";
            }
            if (ktmatkhau != ktrematkhau && !String.IsNullOrEmpty(ktrematkhau))//nếu nhập lại mật khẩu không chính xác
            {
                ViewData["Loi6"] = "Mật khẩu nhập lại không giống nhau";
            }

            KhachHang kh = db.KhachHangs.FirstOrDefault(c => c.UserName == tendn);
            if (kh != null && !String.IsNullOrEmpty(ktmatkhau))//trùng tên đăng nhập
            {
                ViewData["Loi7"] = "Tên đăng nhập đã tồn tại";
            }
            //đã nhập đầy đủ
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn)
                && !String.IsNullOrEmpty(ktmatkhau) && !String.IsNullOrEmpty(dienthoai)
                && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(diachi)
                && matkhau == ktrematkhau)
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
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            return View();
        }
        //Đăng nhập
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //khai báo các biến nhận giá trị từ form f
            //Tên dang nhập
            var tendn = f["UserName"];
            //mật khẩu
            var makhau = nd.PassMD5(f["MatKhau"]);

            //dùng để kiểm tra rỗng
            var ktmatkhau = f["MatKhau"];
            //kiểm tra lỗi chưa nhập
            if (String.IsNullOrEmpty(tendn))//là rỗng
            {
                ViewData["Loi1"] = "Tên đăng nhập không bỏ trống";
            }
            if (String.IsNullOrEmpty(ktmatkhau))//là rỗng
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }

            //nhập đủ
            if (!String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(ktmatkhau))
            {
                //kiểm tra xem có tồn tại khách hàng có taikhoan va matkhau như này ko
                KhachHang k = db.KhachHangs.FirstOrDefault(c => c.UserName == tendn && c.MatKhau == makhau);
                if (k != null)//nếu có tài khoản
                {
                    ViewBag.TB = "Đăng Nhập Thành Công";
                    Session["taikhoan"] = k;//lưu tài khoản lại
                    return RedirectToAction("TrangChu", "Home");//đưa về trang chủ
                }
                else//không có tài khoản trùng
                {
                    ViewBag.TB = "Sai tên Tài khoản hoặc sai mật khẩu, vui lòng đăng nhập lại nhập lại";
                }
            }
            return View();
        }

        //đăng xuất
        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;//làm mới
            Session["GioHang"] = null;//lam moi gio hang
            return RedirectToAction("DangNhap", "NguoiDung");
        }
        //hiển thị menu người dùng và admin
        public ActionResult NguoiDungPartail()
        {
            return PartialView();
        }

        //thông tin người dùng
        [HttpGet]
        public ActionResult ThongTinNguoiDung()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        //Cập Nhật thông tin người dùng
        [HttpGet]
        public ActionResult CapNhatThongtin()
        {
            return View();
        }

        //Cập Nhật thông tin người dùng
        [HttpPost]
        public ActionResult CapNhatThongtin(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)//đã đăng nhập
            {
                //các giá trị người dùng nhập vào
                var hoten = f["HoTen"];
                var dienthoai = f["DienThoai"];
                var email = f["Email"];
                var diachi = f["DiaChi"];

                //kiểm tra lỗi
                if (String.IsNullOrEmpty(hoten))//nếu hoten là rỗng
                {
                    ViewData["L1"] = "Họ tên không dược bỏ trống";
                }
                if (String.IsNullOrEmpty(dienthoai))//nếu là rỗng
                {
                    ViewData["L2"] = "Điện thoại không dược bỏ trống";
                }
                if (String.IsNullOrEmpty(email))//nếu là rỗng
                {
                    ViewData["L3"] = "Email không dược bỏ trống";
                }
                if (String.IsNullOrEmpty(diachi))//nếu là rỗng
                {
                    ViewData["L4"] = "Địa không dược bỏ trống";
                }
                //đã nhập đầy đủ
                if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(dienthoai)
                    && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(diachi))
                {
                    //tìm tài khoản đăng nhập
                    var tk = Session["taikhoan"] as KhachHang;
                    KhachHang k = db.KhachHangs.FirstOrDefault(c => c.UserName == tk.UserName);
                    //gán gá trị cho đối tượng
                    k.HoTen = hoten;
                    k.DienThoai = dienthoai;
                    k.DiaChi = diachi;
                    k.Email = email;

                    //ghi xuống csdl
                    db.KhachHangs.GetModifiedMembers(k);
                    db.SubmitChanges();

                    KhachHang kh = db.KhachHangs.FirstOrDefault(c => c.UserName == tk.UserName);
                    //làm mới lại tài khoản
                    Session["taikhoan"] = null;
                    Session["taikhoan"] = kh;

                    //trở lại trang đăng nhập
                    return RedirectToAction("ThongTinNguoiDung", "NguoiDung");
                }
            }
            else//chưa đăng nhập
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            return View();
        }
        //Đổi Pass
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            //khai báo các biến nhận giá trị từ form f
            //mật khẩu mới
            var matkhau = nd.PassMD5(f["MatKhau"]);
            //nhập lại mật khẩu
            var rematkhau = nd.PassMD5(f["ReMatkhau"]);

            //dùng để kiểm tra rỗng
            var ktmatkhau = f["MatKhau"];
            var ktrematkhau = f["ReMatkhau"];
            //kiểm tra lỗi chưa nhập
            if (String.IsNullOrEmpty(ktmatkhau))//là rỗng
            {
                ViewData["Loi1"] = "Mật Khẩu nhập không bỏ trống";
            }
            if (String.IsNullOrEmpty(ktrematkhau))//là rỗng
            {
                ViewData["Loi2"] = "Vui lòng nhập lại mật khẩu";
            }
            if (ktmatkhau != ktrematkhau && !String.IsNullOrEmpty(ktrematkhau))//nếu nhập lại mật khẩu không chính xác
            {
                ViewData["Loi3"] = "Mật khẩu nhập lại không giống nhau";
            }
            else
            {
                //nhập đủ
                if (!String.IsNullOrEmpty(ktmatkhau) && !String.IsNullOrEmpty(ktrematkhau))
                {
                    var tk = Session["taikhoan"] as KhachHang;
                    //kiểm tra xem có tồn tại khách hàng có taikhoan va matkhau như này ko
                    KhachHang k = db.KhachHangs.FirstOrDefault(c => c.UserName == tk.UserName);
                    if (k != null)//nếu có tài khoản
                    {
                        //gán gá trị cho đối tượng
                        k.MatKhau = matkhau;

                        tk.MatKhau = matkhau;

                        //ghi xuống csdl
                        db.KhachHangs.GetModifiedMembers(k);
                        db.SubmitChanges();

                        return RedirectToAction("TrangChu", "Home");
                    }
                }
            }
            return View();
        }
    }
}