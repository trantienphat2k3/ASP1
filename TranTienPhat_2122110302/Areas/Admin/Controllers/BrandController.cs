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
    public class BrandController : Controller
    {
        WebsiteEcomEntities dbObj = new WebsiteEcomEntities();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var lstBrand = dbObj.Brand.ToArray();
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    // Lấy tên file không có phần mở rộng
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    // Lấy phần mở rộng
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    // Đặt đường dẫn lưu file
                    string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                    // Lưu file vào thư mục
                    objBrand.ImageUpload.SaveAs(path);
                    // Lưu tên file vào cơ sở dữ liệu
                    objBrand.Image = fileName;
                }
                dbObj.Brand.Add(objBrand);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm danh muc. Lỗi: " + ex.Message);
                return View(objBrand);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = dbObj.Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = dbObj.Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);

        }

        [HttpPost]
        public ActionResult Delete(Brand objBra)
        {
            var objBrand = dbObj.Brand.Where(n => n.Id == objBra.Id).FirstOrDefault();
            dbObj.Brand.Remove(objBrand); dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = dbObj.Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);

        }

        [HttpPost]
        public ActionResult Edit(Brand objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                // Lấy tên file không có phần mở rộng
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                // Lấy phần mở rộng
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                // Đặt đường dẫn lưu file
                string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                // Lưu file vào thư mục
                objBrand.ImageUpload.SaveAs(path);
                // Lưu tên file vào cơ sở dữ liệu
                objBrand.Image = fileName;
            }
            dbObj.Entry(objBrand).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}