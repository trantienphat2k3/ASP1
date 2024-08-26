using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranTienPhat_2122110302.DB;

namespace TranTienPhat_2122110302.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}