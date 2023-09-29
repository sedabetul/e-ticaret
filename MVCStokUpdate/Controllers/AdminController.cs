using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using MVCStokUpdate;
using MVCStokUpdate.Models.Entity;


namespace MVCStokUpdate.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin

        [HttpGet]
        [Authorize]
        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPage(TBLMUSTERILER m)
        {
            if (ModelState.IsValid == true)
            {
                db.TBLMUSTERILER.AddOrUpdate(m);

                string ad = m.MUSTERIAD;
                ViewBag.AD = ad;
                string sifre = m.MUSTERISIFRE;
                ViewBag.Sifre = sifre;
                return RedirectToAction("Kategori", "Admin");
            }
            else
            {
                return RedirectToAction("HomePage");
            }




        }


        //public JsonResult kontrol (TBLMUSTERILER model)
        //{
        //    var check=db.TBLMUSTERILER.Where(m=>m.MUSTERIAD==model.MUSTERIAD).Where(m=>m.MUSTERISIFRE==model.MUSTERISIFRE).FirstOrDefault();
        //    if (check!=null)
        //    {
        //        return Json(true,JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult abc(TBLMUSTERILER model)
        //{
        //    //var checkad = db.TBLMUSTERILER.Where(m => m.MUSTERIAD == model.MUSTERIAD).FirstOrDefault();
        //    //var checksifre = db.TBLMUSTERILER.Where(m => m.MUSTERISIFRE == model.MUSTERISIFRE).FirstOrDefault();

        //    //if (model== checkad && model== checksifre)
        //    //{
        //    //    return Json("Kategori");
        //    //}
        //    //else
        //    //{
        //    //    return Json("HomePage");
        //    //}



        //    if (model.MUSTERIAD == "admin" && model.MUSTERISIFRE=="123")
        //    {
        //        TBLMUSTERILER m = new TBLMUSTERILER()
        //        {
        //            MUSTERIAD = model.MUSTERIAD,
        //            MUSTERISIFRE = model.MUSTERISIFRE,
        //        };

        //        return Json("Kategori");
        //    }
        //    else
        //    {
        //        return Json("HomePage");
        //    }
        //}


        public ActionResult abc(TBLMUSTERILER model)
        {
            if (model.MUSTERIAD == "admin" && model.MUSTERISIFRE == "123")
            {
                // Kategori sayfasına yönlendir
                return RedirectToAction("Kategori");
            }
            else
            {
                // Ana sayfaya yönlendir
                return RedirectToAction("HomePage", "User");
            }
        }


        MvcDbStokEntities db = new MvcDbStokEntities();

        
        public ActionResult Kategori()
        {
            if (Session["girisok"] == "1")
            {
                var listele = db.TBLKATEGORILER.ToList();
                return View(listele);
            }
            else if (Session["girisok"] != "1")
            {
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Login", "User");
        }
        

        [HttpGet]

        public ActionResult YeniKategori(int? id)
        {

            var kategori = new TBLKATEGORILER();
            if (id != 0 && id != null)
            {
                kategori = db.TBLKATEGORILER.Where(m => m.KATEGORIID == id).FirstOrDefault();

            }
            return View(kategori);
        }



        [HttpPost]

        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            try
            {
                db.TBLKATEGORILER.AddOrUpdate(p1);
                db.SaveChanges();
                return View();
            }
            catch
            {
                return RedirectToAction("Kategori");
            }

        }



        public ActionResult Sil(int id)
        {


            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Kategori");



        }




        //urunler için olan controller
      
        public ActionResult Urun()
        {
            if (Session["girisok"] == "1")
            {
                var urunliste = db.TBLURUNLER.ToList();
                return View(urunliste);
            }
            else
            {
                return RedirectToAction("HomePage", "Admin");
            }

        }

        [HttpGet]

        public ActionResult YeniUrun(int? id)
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;

            var urun = new TBLURUNLER();
            if (id != 0 && id != null)
            {
                urun = db.TBLURUNLER.Where(k => k.URUNID == id).FirstOrDefault();
            }
            return View(urun);
        }


        [HttpPost]

        public ActionResult YeniUrun(TBLURUNLER u1)
        {
            db.TBLURUNLER.AddOrUpdate(u1);
            db.SaveChanges();
            return View();
        }


        public ActionResult USil(int id)
        {
            var kategori = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Urun");
        }


        //musterı controller

        
        public ActionResult Musteri()
        {
            if (Session["girisok"] == "1")
            {
                var musteri = db.TBLMUSTERILER.ToList();
                return View(musteri);
            }
            else
            {
                return RedirectToAction("HomePage", "User");
            }

        }


        [HttpGet]

        public ActionResult YeniMusteri(int? id)
        {
            var musteri = new TBLMUSTERILER();
            if (id != 0 && id != null)
            {
                musteri = db.TBLMUSTERILER.Where(m => m.MUSTERIID == id).FirstOrDefault();
            }
            return View(musteri);
        }

        [HttpPost]

        public ActionResult YeniMusteri(TBLMUSTERILER m)
        {
            db.TBLMUSTERILER.AddOrUpdate(m);
            db.SaveChanges();
            return View();
        }




        public ActionResult MSil(int id)
        {
            var sil = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Musteri");
        }







        public ActionResult cikis()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage");



        }









    }
}