using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTienPhat_2122110302.DB;

namespace TranTienPhat_2122110302.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        WebsiteEcomEntities db = new WebsiteEcomEntities();
        // GET: Prooduct
        public ActionResult Detail(int id)
        {
            var detail = db.Product.Find(id);
            return View(detail);
        }


    }
}