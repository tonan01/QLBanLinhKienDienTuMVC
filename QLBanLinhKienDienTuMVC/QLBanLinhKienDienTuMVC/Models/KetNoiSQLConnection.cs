using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanLinhKienDienTuMVC.Models
{
    public class KetNoiSQLConnection
    {
        //kết nối
        public  string ketNOI()
        {
            return "Data Source = DESKTOP-K7BRREB; database = QLLinhKienDienTu; User ID = sa; Password = 123";
        }
    }
}