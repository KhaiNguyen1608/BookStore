using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

using PagedList;
using PagedList.Mvc;

namespace MvcBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: BookStore

        private List<SACH> Laysachmoi(int count )
        {
            
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int? page, string searchString)
        {
            var sp = from e in data.SACHes select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.Tensach.Contains(searchString));
            }
            ViewBag.SeachString = searchString;
            //tạo bien so trang
            int pageNumber = (page ?? 1);
            //tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            return View(sp.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
            //return View(db.SAN_PHAMs.ToList());
        }

        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult NhaXuatban ()
        {
            var nhaxuatban = from cd in data.NHAXUATBANs select cd;
            return PartialView(nhaxuatban);
        }

        public ActionResult SPTheochude(int id)
        {
            var sach=from s in data.SACHes where s.MaCD==id select s;
            return View(sach);
        }
        public ActionResult SPNhaXuatBan(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult Details (int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}