using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOUR.Models;
using System.Data.SqlClient;

namespace TOUR.Controllers
{
    public class ACCOUNTController : Controller
    {

        QLDatTourEntities1 db = new QLDatTourEntities1();
        [HttpGet]
        // GET: ACCOUNT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy(string id= "")
        {
            TaiKhoan emp = new TaiKhoan();
            var lastuser = db.TaiKhoans.OrderByDescending(c => c.IdUser).FirstOrDefault();
            if(id !="")
            {
                emp = db.TaiKhoans.Where(x => x.IdUser == id).FirstOrDefault<TaiKhoan>();
            }    
             else if (lastuser==null)
            {
                emp.IdUser = "TK001";
            }   
            else
            {
                emp.IdUser = "TK" + (Convert.ToInt32(lastuser.IdUser.Substring(3, lastuser.IdUser.Length - 3)) + 1).ToString("D3");
            }
            return View(emp);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(TaiKhoan tk)
        {
            
            if (db.TaiKhoans.Any(x => x.UserName == tk.UserName))
            {
                ViewBag.ErrorRegister = "Tài Khoản Này Đã Tồn Tại.";
                return View();
            }
            else
            {
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
            }
            

            ViewBag.SuccessMessage = "Đăng Ký Thành Công.";
            return View(tk);
        }



        public ActionResult DangXuat()
        {
            Session["UserName"] = null;
            return Redirect("Index");
        }

        

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan _user)
        {
            var check = db.TaiKhoans.Where(s => s.UserName == _user.UserName
            && s.PassUser == _user.PassUser).FirstOrDefault();
            
            if (check == null)
            {
                ViewBag.ErrorInfo = "Mật khẩu hay tài khoản đã sai. Vui lòng nhập lại.";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["UserName"] = _user.UserName;
                Session["PassUser"] = _user.PassUser;
                return RedirectToAction("Index", "TOUR");
            }
        }
    }
}
