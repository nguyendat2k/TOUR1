using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOUR.Models;

namespace TOUR.Controllers
{
    public class KHACHSANController : Controller
    {
        QLDatTourEntities db = new QLDatTourEntities();

        // GET: KHACHSAN
        private List<KhachSan> LayKhachSanMoi(int soluong)
        {
            return db.KhachSans.OrderByDescending(n => n.MaKS).ToList();
        }
        public ActionResult Index()
        {
            return View(LayKhachSanMoi(10));
        }
        public ActionResult Details(string id)
        {
            var ks = (from s in db.KhachSans
                        where s.MaKS == id
                        select new ViewKhachSan()
                        {
                            maks = s.MaKS,
                            tenks = s.TenKS,
                            diachiks = s.DiaChiKS,
                            anhks = s.AnhKS,
                            mahang = s.MaHang,                         
                        }).SingleOrDefault();
            return View(ks);
        }
    }
}