using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using TranTienPhat_2122110302.DB;

namespace TranTienPhat_2122110302.Controllers
{
    public class UserController : Controller
    {
        WebsiteEcomEntities db = new WebsiteEcomEntities();

        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objUser)
        {
            objUser.password = CreateMD5(objUser.password);
            var FlagUser = db.User.Where(n => n.email == objUser.email && n.password == objUser.password).ToList();
            if (FlagUser.Count > 0)
            {
                Session["username"] = FlagUser.FirstOrDefault().email.ToString();
                Session["idUser"] = FlagUser.FirstOrDefault().userid;
            }
            return RedirectToAction("Index", "Home");
        }

        public static string CreateMD5(string input)
        {
            // Sử dụng chuỗi đầu vào để tính toán hash MD5
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành chuỗi dạng hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public ActionResult Logout()
        {
            // Xoá tất cả session
            Session.Clear();

            // Hoặc chỉ xoá session "username"
            // Session["username"] = null;

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "User");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Mã hóa mật khẩu bằng MD5
                    objUser.password = CreateMD5(objUser.password);

                    // Thêm người dùng vào cơ sở dữ liệu
                    db.User.Add(objUser);
                    db.SaveChanges();

                    // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                Console.WriteLine(ex.Message);
                // Bạn có thể thêm thông báo lỗi vào ModelState để hiển thị thông báo lỗi trên view
                ModelState.AddModelError("", "Đã có lỗi xảy ra. Vui lòng thử lại.");
            }

            return View(objUser);
        }

        //public static string CreateMD5(string input)
        //{
        //    // Sử dụng chuỗi đầu vào để tính toán hash MD5
        //    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        //    {
        //        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //        byte[] hashBytes = md5.ComputeHash(inputBytes);

        //        // Chuyển đổi mảng byte thành chuỗi dạng hexadecimal
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < hashBytes.Length; i++)
        //        {
        //            sb.Append(hashBytes[i].ToString("X2"));
        //        }
        //        return sb.ToString();
        //    }
        //}
    }

}