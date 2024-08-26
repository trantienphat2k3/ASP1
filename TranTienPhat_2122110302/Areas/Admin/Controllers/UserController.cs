using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TranTienPhat_2122110302.DB;

namespace TranTienPhat_2122110302.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebsiteEcomEntities dbObj = new WebsiteEcomEntities();
        // GET: Admin/User
        public ActionResult Index()
        {
            var lstUser = dbObj.User.ToArray();
            return View(lstUser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            try
            {

                objUser.password = CreateMD5(objUser.password);
                dbObj.User.Add(objUser);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm . Lỗi: " + ex.Message);
                return View(objUser);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = dbObj.User.Where(n => n.userid == id).FirstOrDefault();
            return View(objUser);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            // Tìm User theo id
            var objUser = dbObj.User.FirstOrDefault(n => n.userid == id);

            // Kiểm tra nếu User không tồn tại
            if (objUser == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy User
            }

            return View(objUser);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Tìm User theo id
            var objUser = dbObj.User.FirstOrDefault(n => n.userid == id);

            // Kiểm tra nếu User không tồn tại
            if (objUser == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy User
            }

            // Xóa User và lưu thay đổi
            dbObj.User.Remove(objUser);
            dbObj.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = dbObj.User.Where(n => n.userid == id).FirstOrDefault();
            return View(objUser);

        }

        [HttpPost]
        public ActionResult Edit(User objUser)
        {

            objUser.password = CreateMD5(objUser.password);
            dbObj.Entry(objUser).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");

        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToHexString(hashBytes); 
                // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder obj = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    obj.Append(hashBytes[i].ToString("X2"));
                }
                return obj.ToString();
            }
        }
    }

}