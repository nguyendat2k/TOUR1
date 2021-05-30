﻿using TOUR.Models;
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
        public ActionResult Suatour(string id)
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
        [ValidateInput(false)]
        public ActionResult Suatour([Bind(Include = "TenTour,MoTa,SoNgay,Gia,NgayKhoiHanh,Anhbia")] Tour tour, HttpPostedFileBase fileupload)
        {
            try
            {
                if (fileupload == null && fileupload.ContentLength > 0)
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
                return View(tour);
            }
        }
        public ActionResult Delete(string id)
        {
            var tour = db.Tours.Select(p => p).Where(p => p.MaTour == id).FirstOrDefault();
            return View(tour);
        }
        [HttpPost]
        public ActionResult Delete(string id, Tour s)
        {
            
                s = db.Tours.Select(p => p).Where(p => p.MaTour == id).FirstOrDefault();
                {
                    db.Entry(s).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                return RedirectToAction("Tour", "ADMIN");
            }

        
    }
}