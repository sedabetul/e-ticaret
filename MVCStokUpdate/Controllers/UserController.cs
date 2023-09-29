using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MVCStokUpdate;
using System.Web.Razor.Generator;
using MVCStokUpdate.Models;
using MVCStokUpdate.Models.Entity;

namespace MVCStokUpdate.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MvcDbStokEntities db = new MvcDbStokEntities();
       

        public ActionResult HomePage()
        {
            var urunlerılısteleanasayfa = db.TBLURUNLER.ToList();
            return View(urunlerılısteleanasayfa);
        }


        //KAYIT OLMA KONTROLLERİ


        [HttpGet]
        public ActionResult Kayitol()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Kayitol(TBLMUSTERILER m1, FormCollection fc)
        {
            if (m1.MUSTERIAD == "admin" && m1.MUSTERISIFRE == "123")
                {
                    // Kategori sayfasına yönlendir
                    return RedirectToAction("Kategori","Admin");
                }
              
            if (ModelState.IsValid)
            {
               
              
                if (string.IsNullOrEmpty(m1.MUSTERIMAIL))
                {
                    ModelState.AddModelError("MUSTERIMAIL", " Required");
                  
                }

                db.TBLMUSTERILER.Add(m1); 
                db.SaveChanges();
                return RedirectToAction("HomePage");
            }
            else
            {
                return View();
            }


        }


        


     public ActionResult log(TBLMUSTERILER m)
        {
            var bilgi=db.TBLMUSTERILER.FirstOrDefault(a=>a.MUSTERIAD==m.MUSTERIAD && a.MUSTERISIFRE==m.MUSTERISIFRE);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.MUSTERIAD, false);
                Session["MUSTERIAD"] = bilgi.MUSTERIAD.ToString();
                return RedirectToAction("Kayitol");
            }

         else { return View("HomePage"); }


        }







        public ActionResult LogOut()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage");
        }

      
      
        public ActionResult Index()
        {
            return View(getsepet());
        }


        public ActionResult sepeteekle(int Id)
        {
            var urun = db.TBLURUNLER.Where(i => i.URUNID == Id).FirstOrDefault();
            if (urun != null)
            {
                getsepet().sepet_urun_ekle(urun,1);
            }
            return RedirectToAction("Index");


        }




        public ActionResult sepetsil(int Id)
        {
            var urun = db.TBLURUNLER.Where(i => i.URUNID == Id).FirstOrDefault();
            if (urun != null)
            {
                getsepet().sepet_urun_sil(urun);
            }
            return RedirectToAction("Index");
        }




        public sepet getsepet()
        {
            sepet sepet = (sepet)Session["sepet"];
            if (sepet == null)
            {
                sepet = new sepet();
                Session["sepet"] = sepet;
            }
            return sepet;
        }





        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(TBLMUSTERILER yonlendir)
        {
            
            if (yonlendir.MUSTERIAD=="admin" && yonlendir.MUSTERISIFRE == "123")
            { Session["girisok"] = "1";
                return RedirectToAction("Kategori","Admin"); 
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }






    }
}
  