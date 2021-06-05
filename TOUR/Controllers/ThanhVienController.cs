using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOUR.Models;

namespace TOUR.Controllers
{
    public class ThanhVienController : Controller
    {
        private QLDatTourEntities1 db = new QLDatTourEntities1();
        // GET: ThanhVien
        public ActionResult Index()
        {
            return View();
        }
        // GET: ThanhVien/Create
        public ActionResult Create(string id = "")
        {
            //ViewBag.PhieuDoans = db.PhieuDoans.OrderBy(c => c.SoNguoi).ToList();
            ViewBag.MaDoan = new SelectList(db.PhieuDoans.ToList().OrderBy(n => n.TenCQ), "MaDoan", "MaDoan");
            ThanhVien emp = new ThanhVien();
            var lasttv = db.ThanhViens.OrderByDescending(c => c.STT).FirstOrDefault();
            if (id != "")
            {
                emp = db.ThanhViens.Where(x => x.STT == id).FirstOrDefault<ThanhVien>();
            }
            else if (lasttv == null)
            {
                emp.STT = "TV001";
            }
            else
            {
                emp.STT = "TV" + (Convert.ToInt32(lasttv.STT.Substring(3, lasttv.STT.Length - 3)) + 1).ToString("D3");
            }
            
            
            return View(emp);

        }

        // POST: PhieuDoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                Cart cart = Session["Cart"] as Cart;
                db.ThanhViens.Add(thanhVien);
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("DangKyThanhCong","ThanhVien");
            }

            return View(thanhVien);
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
    }
}
