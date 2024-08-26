using TranTienPhat_2122110302.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TranTienPhat_2122110302.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteEcomEntities dbObj = new WebsiteEcomEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var lstCategory = dbObj.Category.ToArray();
            return View(lstCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    // Lấy tên file không có phần mở rộng
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    // Lấy phần mở rộng
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    // Đặt đường dẫn lưu file
                    string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                    // Lưu file vào thư mục
                    objCategory.ImageUpload.SaveAs(path);
                    // Lưu tên file vào cơ sở dữ liệu
                    objCategory.Image = fileName;
                }
                dbObj.Category.Add(objCategory);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm danh muc. Lỗi: " + ex.Message);
                return View(objCategory);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = dbObj.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = dbObj.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = dbObj.Category.Where(n => n.Id == objCate.Id).FirstOrDefault();
            dbObj.Category.Remove(objCategory); dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = dbObj.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                // Lấy tên file không có phần mở rộng
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                // Lấy phần mở rộng
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                // Đặt đường dẫn lưu file
                string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                // Lưu file vào thư mục
                objCategory.ImageUpload.SaveAs(path);
                // Lưu tên file vào cơ sở dữ liệu
                objCategory.Image = fileName;
            }
            dbObj.Entry(objCategory).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}