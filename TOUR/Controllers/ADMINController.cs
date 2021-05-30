using TOUR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace TOUR.Controllers
{
    public class ADMINController : Controller
    {
        QLDatTourEntities db = new QLDatTourEntities();
        // GET: ADMIN
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gần giá trị cho các đối tượng được tạo mới 
                Admin ad = db.Admins.SingleOrDefault(n => n.User == tendn && n.Pass == matkhau);
                if (ad != null)
                {
                    Session["TaikhoanAdmin"] = ad;
                    return RedirectToAction("sach");
                }
            }
            ViewBag.Thongbao = "Ten dang nhap hoac mat khau khong dung";
            return View();
        }
        public ActionResult Tour(int ?page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 7;
            return View(db.Tours.ToList().OrderBy(n=>n.MaTour).ToPagedList(pageNum,pageSize));
        }

        public ActionResult Themmoitour()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoitour(Tour tour, HttpPostedFileBase fileupload)
        {
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    tour.Anhbia = filename;
                    db.Tours.Add(tour);
                    db.SaveChanges();                 
                }
            }
            return RedirectToAction("Tour");
        }
        public ActionResult Detail(string id)
        {
            Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == id);
            ViewBag.MaSach = tour.MaTour;
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }
    }
}