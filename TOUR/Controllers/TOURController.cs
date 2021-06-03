using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOUR.Models;

namespace TOUR.Controllers
{
    public class TOURController : Controller
    {
        QLDatTourEntities1 db = new QLDatTourEntities1();
        // GET: TOUR
        private List<Tour> LayTourMoi(int soluong)
        {
            return db.Tours.OrderByDescending(n => n.MaTour).ToList();
        }
        public ActionResult Index()
        {
            return View(LayTourMoi(6));
        }
        public ActionResult Details(string id)
        {
            var tour = (from s in db.Tours
                        where s.MaTour == id
                        select new ViewTour()
                        {
                            matour = s.MaTour,
                            tentour = s.TenTour,
                            mota = s.MoTa,
                            songay = s.SoNgay,
                            gia = s.Gia,
                            ngaykhoihanh = (System.DateTime)s.NgayKhoiHanh,
                            anhbia = s.Anhbia
                        }).SingleOrDefault();
            return View(tour);
        }    
    }
}