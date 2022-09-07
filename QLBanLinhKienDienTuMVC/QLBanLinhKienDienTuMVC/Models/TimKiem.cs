using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class TimKiem
    {
        ////ham ket noi sql
        //public string conTr =  "Data Source=DESKTOP-K7BRREB; database=QLLinhKienDienTu; User ID=sa;Password=123";
        KetNoiSQLConnection kn = new KetNoiSQLConnection();
        //tìm kiếm
        public List<SanPham> getData(string timkiem)
        {
            //khoi tao list
            List<SanPham> listSanPham = new List<SanPham>();
            //ket noi sql
            SqlConnection con = new SqlConnection(kn.ketNOI());
            //cau truy van
            string sql = "exec TimKiem N'%" + timkiem + "%'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();//mo sql
            SqlDataReader rdr = cmd.ExecuteReader();//select        neu
            while (rdr.Read())
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToInt32(rdr.GetValue(0).ToString());
                sp.TenSP = rdr.GetValue(1).ToString();
                sp.GiaBan = int.Parse(rdr.GetValue(2).ToString());
                sp.MoTa = rdr.GetValue(3).ToString();
                sp.Anh = rdr.GetValue(4).ToString();
                sp.SLTon = Convert.ToInt32(rdr.GetValue(5).ToString());
                sp.MaLoai = Convert.ToInt32(rdr.GetValue(6).ToString());
                listSanPham.Add(sp);//them vao list
            }
            return (listSanPham);//tra ve list

        }
    }
}