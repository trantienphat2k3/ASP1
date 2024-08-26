using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTienPhat_2122110302.Models;
using TranTienPhat_2122110302.DB;


namespace TranTienPhat_2122110302.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            //HomeModel homeModel = new HomeModel();
            //homeModel.ListCategory = objWebsiteEcomEntities.Category.ToList();
            var lstCategory = objWebsiteEcomEntities.Category.ToList();
            return View(lstCategory);
            //return View(homeModel);
        }
        public ActionResult ProductByCategoryID(int id)
        {
            var lstproduct = objWebsiteEcomEntities.Product.Where(n => n.CategoryId == id).ToList();
            return View(lstproduct);

        }
    }
}