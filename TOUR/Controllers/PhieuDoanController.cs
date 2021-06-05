using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOUR.Models;

namespace TOUR.Controllers
{
    public class PhieuDoanController : Controller
    {
        private QLDatTourEntities1 db = new QLDatTourEntities1();
        // GET: PhieuDoan
        public ActionResult Index()
        {
            return View();
        }

        // GET: PhieuDoan/Create
        public ActionResult Create(string id = "")
        {
            ViewBag.MaTour = new SelectList(db.Tours.ToList().OrderBy(n => n.TenTour), "MaTour", "TenTour");
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.TenNV), "MaNV", "TenNV");
            PhieuDoan emp = new PhieuDoan();
            var lastdoan = db.PhieuDoans.OrderByDescending(c => c.MaDoan).FirstOrDefault();
            if (id != "")
            {
                emp = db.PhieuDoans.Where(x => x.MaDoan == id).FirstOrDefault<PhieuDoan>();
            }
            else if (lastdoan == null)
            {
                emp.MaDoan = "D001";
            }
            else
            {
                emp.MaDoan = "D" + (Convert.ToInt32(lastdoan.MaDoan.Substring(2, lastdoan.MaDoan.Length - 2)) + 1).ToString("D3");
            }
            return View(emp);

        }

        // POST: PhieuDoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhieuDoan phieuDoan)
        {
            
            if (ModelState.IsValid)
            {
               
                db.PhieuDoans.Add(phieuDoan);
                db.SaveChanges();
                return RedirectToAction("Create","ThanhVien");
            }

            return View(phieuDoan);
        }
    }
}