using TranTienPhat_2122110302.DB;
using TranTienPhat_2122110302.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using PagedList;
using System.Linq;

using System.Web.Mvc;

namespace TranTienPhat_2122110302.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteEcomEntities dbObj = new WebsiteEcomEntities();

        // GET: Admin/Product
        public ActionResult Index(string searchString, int? page)
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = dbObj.Product.AsQueryable();

            // Lọc theo tên sản phẩm nếu có tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(n => n.Name.Contains(searchString));
            }

            // Sắp xếp sản phẩm theo ID trước khi phân trang
            products = products.OrderBy(p => p.Id); // Hoặc sắp xếp theo thuộc tính khác

            // Thiết lập số lượng sản phẩm trên mỗi trang và trang hiện tại
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Phân trang
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            // Truyền biến tìm kiếm hiện tại đến view
            ViewBag.CurrentFilter = searchString;

            return View(pagedProducts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    // Lấy tên file không có phần mở rộng
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    // Lấy phần mở rộng
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    // Đặt đường dẫn lưu file
                    string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                    // Lưu file vào thư mục
                    objProduct.ImageUpload.SaveAs(path);
                    // Lưu tên file vào cơ sở dữ liệu
                    objProduct.Image = fileName;
                }
                dbObj.Product.Add(objProduct);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm sản phẩm. Lỗi: " + ex.Message);
                return View(objProduct);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = dbObj.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = dbObj.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);

        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = dbObj.Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            dbObj.Product.Remove(objProduct); dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = dbObj.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);

        }

        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                // Lấy tên file không có phần mở rộng
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                // Lấy phần mở rộng
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                // Đặt đường dẫn lưu file
                string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                // Lưu file vào thư mục
                objProduct.ImageUpload.SaveAs(path);
                // Lưu tên file vào cơ sở dữ liệu
                objProduct.Image = fileName;
            }
            dbObj.Entry(objProduct).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
    }

}