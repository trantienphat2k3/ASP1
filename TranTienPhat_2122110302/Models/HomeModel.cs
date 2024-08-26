using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranTienPhat_2122110302.DB;

namespace TranTienPhat_2122110302.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }


    }
}