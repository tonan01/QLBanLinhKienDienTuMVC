using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class ThanhToan
    {
        ////ham ket noi sql
        //public string conTr = "Data Source=DESKTOP-K7BRREB; database=QLLinhKienDienTu; User ID=sa;Password=123";
        KetNoiSQLConnection kn = new KetNoiSQLConnection();
        int md = 0;
        //Thanh Toan
        public int ThanhToanTien(int maDH, string UserN, int MaSP, int sLuong)
        {
            
            
            SqlConnection con = new SqlConnection(kn.ketNOI());
            con.Open();
            int rs = 0;
            string sql = "exec ThanhToanDH '" + maDH + "','" + UserN + "'," + MaSP + ",'" + sLuong + "'";

            SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                rs = cmd.ExecuteNonQuery();//thuc hien cau lenh them xoa sua
                con.Close();
                return rs;//thành công nếu thành công trả về 1
        }

        //lấy mã DH của dh vừa tạo
        public int LayMaDH(string Username)
        {
            if (md != 0)
            {
                SqlConnection con = new SqlConnection(kn.ketNOI());
                con.Open();
                //kiem tra trung ten
                string sql1 = "select MAX(MaDonHang) from DonHang where UserName='" + Username + "'";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                int kt = (int)cmd1.ExecuteScalar();//xem co bao nhieu ten nhan vien
                con.Close();
                return kt;//Mã Số hóa đơn người dùng vừa tạo
            }
            else
            {
                md++;
                return 0;
            }
        }
    }
}