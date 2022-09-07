using QLBanLinhKienDienTuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanLinhKienDienTuMVC.Controllers
{
    public class Ad_DonHangController : KiemTraDangNhapAdminController
    {
        // GET: Ad_DonHang
        // GET: Ad_SanPham
        dbQLLinhKienDataContext db = new dbQLLinhKienDataContext();

        //danh sach DonHang
        public ActionResult DanhSachDonHang(FormCollection f)
        {
            var LstDonHang = db.DonHangs.OrderBy(c => c.MaDonHang).ToList();
            return View(LstDonHang);
        }

        //hiển thị sản phẩm tìm kiếm
        public ActionResult TimKiemDonHang(FormCollection f)
        {
            Ad_DonHang d = new Ad_DonHang();
            var lisDonHang = d.getData(f["timkiem"].ToString());
            //nếu không có sản phẩm nào
            if (lisDonHang.Count == 0)
            {
                var LstDonHang1 = db.DonHangs.OrderBy(c => c.MaDonHang).ToList();
                return View(LstDonHang1);
            }
            return View(lisDonHang);
        }

        //Giao hàng
        public ActionResult GiaoHang(int MaDonHang)
        {
            //các giá trị người dùng nhập vào
            DateTime ngaygiao = DateTime.Now;
            //kiểm tra lỗi
            DonHang dh = db.DonHangs.Single(c =>c.MaDonHang==MaDonHang && c.NgayGiao == null);
            if(dh==null)//đơn hàng đã dc giao
            {
                ViewData["Loi1"] = "Đơn hàng đã được giao";
            }
            //đã nhập đầy đủ
            if (dh!=null)
            {
                DonHang k = db.DonHangs.FirstOrDefault(c => c.MaDonHang == MaDonHang);
                //gán gá trị cho đối tượng
                k.NgayGiao = ngaygiao;
                k.TinhTrangGiaoHang = "Hoàn Thành";

                //ghi xuống csdl
                db.DonHangs.GetModifiedMembers(k);
                db.SubmitChanges();
                //trở lại trang đăng nhập
                return RedirectToAction("DanhSachDonHang", "Ad_DonHang");
            }
            return View();
            
        }

        //Xóa Don hàng
        public ActionResult HuyDonHang(int MaDonHang)
        {
            try//thành công
            {
                DonHang ktdh = db.DonHangs.FirstOrDefault(c => c.MaDonHang == MaDonHang && c.TinhTrangGiaoHang =="Đang Xử Lý");
                //Đơn hàng hoàn thành
                if(ktdh==null)
                {
                    Ad_DonHang dt = new Ad_DonHang();
                    //xóa
                    if(dt.XoaDonHang(MaDonHang)>0)//thành công trả về 1
                    {
                        ViewBag.ER = "Thành Công";
                    }
                    else//thất bại 0
                    {
                        ViewBag.ER = "Thất Bại";
                        return View();
                    }
                }
                else//đơn hàng đang sử lý
                {
                    ViewBag.Loi = "Đơn Hàng đang xử lý không thể xóa";
                    return View();
                }

                return RedirectToAction("DanhSachDonHang", "Ad_DonHang");
            }
            catch//thất Bại
            {
                ViewBag.TBTrung = "Lỗi Xóa";
                return View();
            }
        }
    }
}