using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class Ad_DonHang
    {
        KetNoiSQLConnection kn = new KetNoiSQLConnection();
        //Thanh Toan
        public int XoaDonHang(int MaDonHang)
        {
            SqlConnection con = new SqlConnection(kn.ketNOI());
            con.Open();
            int rs = 0;
            string sql = "exec HuyDonHang '" + MaDonHang + "'";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            rs = cmd.ExecuteNonQuery();//thuc hien cau lenh them xoa sua
            con.Close();
            return rs;//thành công nếu thành công trả về 1
        }

        //tim kiếm
        public List<DonHang> getData(string timkiem)
        {
            //khoi tao list
            List<DonHang> listDonHang = new List<DonHang>();
            //ket noi sql
            SqlConnection con = new SqlConnection(kn.ketNOI());
            //cau truy van
            string sql = "select * from DonHang where TinhTrangGiaoHang=N'"+timkiem+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();//mo sql
            SqlDataReader rdr = cmd.ExecuteReader();//select        neu
            while (rdr.Read())
            {
                DonHang sp = new DonHang();
                sp.MaDonHang = Convert.ToInt32(rdr.GetValue(0).ToString());
                try//dã giao hàng
                {
                    sp.NgayGiao = DateTime.Parse(rdr.GetValue(1).ToString());
                }
                catch//chưa giao hàng
                {

                }
                sp.NgayDat = DateTime.Parse(rdr.GetValue(2).ToString());
                sp.TinhTrangGiaoHang = rdr.GetValue(3).ToString();
                sp.UserName = rdr.GetValue(4).ToString();
                listDonHang.Add(sp);//them vao list
            }
            return (listDonHang);//tra ve list

        }
    }
}