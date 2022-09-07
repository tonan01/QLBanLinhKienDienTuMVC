using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class NguoiDung
    {
        public  string PassMD5(string MatKhau)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //Chuyển kiểu chuổi thành kiểu byte
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(MatKhau));
            //mã hóa chuỗi đã chuyển
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}