using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();
        //lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //nếu chưa có sản phẩm
            if(lstGioHang==null)
            {
                //tạo giỏ hàng
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //thêm sản phẩm vào giỏ
        public ActionResult ThemGioHang(int MaSP,string strURL)
        {
            //nếu đã đăng nhập
            if(Session["TaiKhoan"]!=null)
            {
                List<GioHang> lstGioHang = LayGioHang();
                //tìm sản phảm trong giỏ hàng
                GioHang gh = lstGioHang.Find(c => c.iMaSP == MaSP);
                //không có sản phảm này trong giỏ hàng
                if(gh==null)
                {
                    //thêm sản phẩm vào giỏ
                    gh = new GioHang(MaSP);
                    lstGioHang.Add(gh);
                    return Redirect(strURL);
                }
                else//đã có sản phẩm này trong giỏ hàng
                {
                    gh.iSoLuong++;//cộng thêm 1 số lượng
                    return Redirect(strURL);
                }
            }
            else//chưa đăng nhập
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
        }

        //tổng số lượng hàng trong giỏ
        public int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang!=null)//co sản phẩm trông giỏ
            {
                tsl = lstGioHang.Sum(s => s.iSoLuong);//tổng số lượng sản phẩm cộng lại
            }
            return tsl;
        }

        //tổng thành tiền
        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt = lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }

        //giỏ hàng
        public ActionResult GioHang()
        {
            //chưa đăng nhập
            if(Session["GioHang"] ==null)
            {
                return RedirectToAction("ThongBaoKhongCoHang", "Home");
            }
            //xem giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //cập nhật số lượng sản phẩm
            ViewBag.TongSoLuong = TongSoLuong();
            //cập nhật tổng tiền
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);
        }

        //xem giỏ bằng icon
        public ActionResult GioHangPartial()
        {
            //cập nhật số lượng sản phẩm
            ViewBag.TongSoLuong = TongSoLuong();
            //cập nhật tổng tiền
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }

        //thanh toán
        public ActionResult ThanhToan()
        {
            ViewBag.TongSoLuong = 0;
            ViewBag.TongThanhTien = 0;
            //làm mới giỏ hàng
            Session["GioHang"] = null;
            return View();
        }

        //xóa 1 sản phẩm trông giỏ
        public ActionResult XoaGioHang(int MaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if(sp!=null)// có sản phẩm đó
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP);
                return RedirectToAction("GioHang", "GioHang");
            }
            else//không có sản phẩm đó
            {
                return RedirectToAction("ThongBaoKhongCoHang", "Home");
            }
        }

        //xóa tất cả sản phẩm có trông giỏ
        public ActionResult XoaGioHang_ALL()
        {
            List<GioHang> lstGioHang = LayGioHang();
            //xóa tất cả trông giỏ
            lstGioHang.Clear();
            return RedirectToAction("ThongBaoKhongCoHang", "Home");
        }

        //update số lượng sản phẩm có trong giỏ
        public ActionResult CapNhatGioHang(int MaSP,FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if(sp!=null)//có sản phẩm
            {
                SanPham sanpham = db.SanPhams.FirstOrDefault(c => c.MaSP == MaSP && c.SLTon >= int.Parse(f["txtSoLuong"].ToString()));
                if(sanpham!=null)//số lượng tồn kho đủ
                {
                    //cập nhật lại số lượng
                    sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                }
                else//số lượng tồn kho không đủ
                {
                    ViewBag.SL = "red";
                }
                
            }
            return RedirectToAction("GioHang", "GioHang");

        }


    }
}