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
        QLDatTourEntities1 db = new QLDatTourEntities1();
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
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["TaikhoanAdmin"] = ad;
                    return RedirectToAction("sach");
                }
            }
            ViewBag.Thongbao = "Ten dang nhap hoac mat khau khong dung";
            return View();
        }
        public ActionResult Tour(int ?page, string searchby, string search)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            if (searchby == "tentour")
                return View(db.Tours.Where(s => s.TenTour.StartsWith(search)).ToList().ToPagedList(pageNumber, pageSize));        
            return View(db.Tours.ToList().OrderBy(n => n.MaTour).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create(string id = "")
        {
            Tour emp = new Tour();
            var lasttour = db.Tours.OrderByDescending(c => c.MaTour).FirstOrDefault();
            if (id != "")
            {
                emp = db.Tours.Where(x => x.MaTour == id).FirstOrDefault<Tour>();
            }
            else if (lasttour == null)
            {
                emp.MaTour = "T001";
            }
            else
            {
                emp.MaTour = "T" + (Convert.ToInt32(lasttour.MaTour.Substring(2, lasttour.MaTour.Length - 2)) + 1).ToString("D3");
            }
            return View(emp);

        }
        [HttpPost]
        public ActionResult Create(Tour tour, HttpPostedFileBase fileupload)
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
        public ActionResult Edit(string id)
        {

            Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == id);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }
        [HttpPost]

        public ActionResult Edit( Tour tour, HttpPostedFileBase fileupload)
        {
            try
            {
                if (fileupload != null && fileupload.ContentLength > 0)
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
                }
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Tour");
            }
            catch
            {
                return View();
            }
        }
     public ActionResult Delete(string id)
        {
            Tour t = db.Tours.SingleOrDefault(n => n.MaTour == id);
            if(t==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteConfirm(string MaTour)
        {
            /*try
            {
                tour = db.Tours.SingleOrDefault(n => n.MaTour == id);
                db.Entry(tour).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Tour");
            }
            catch
            {
                return View();
            }*/
            try
            {
                Tour tour = db.Tours.Find(MaTour);
                db.Tours.Remove(tour);
                db.SaveChanges();
                return RedirectToAction("Tour");
            }
            catch
            {
                ViewBag.Error = "Server can not remove";
                return RedirectToAction("Delete", new { id = MaTour.Trim() });
            }
        }
        
    }
}