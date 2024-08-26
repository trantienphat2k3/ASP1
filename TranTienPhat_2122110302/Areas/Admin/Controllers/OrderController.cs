using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTienPhat_2122110302.DB;
namespace TranTienPhat_2122110302.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteEcomEntities dbObj = new WebsiteEcomEntities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var lstOrder = dbObj.Order.ToArray();
            return View(lstOrder);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = dbObj.Order.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);


        }
    }
}