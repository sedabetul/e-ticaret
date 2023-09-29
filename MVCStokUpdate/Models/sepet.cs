using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using Microsoft.Ajax.Utilities;
using MVCStokUpdate;
using MVCStokUpdate.Models.Entity;

namespace MVCStokUpdate.Models
{
    public class sepet
    {
        private List<sepetherurun> urunler = new List<sepetherurun>();
        public List<sepetherurun> Urunler
        {
            get
            {
                return urunler;
            }
        }

        public void sepet_urun_ekle(TBLURUNLER p1, int adet)
        {
           
            var ara = urunler.Where(i => i.TBLURUNLER.URUNID == p1.URUNID).FirstOrDefault();
            if (ara == null)
            {
                urunler.Add(new sepetherurun() { TBLURUNLER = p1, adet = 1 });  
            }
            else
            {
          
                ara.adet += adet;

            }
        }

       

        public void sepet_urun_sil(TBLURUNLER p1)
        {
            urunler.RemoveAll(i => i.TBLURUNLER.URUNID == p1.URUNID);
        }

        public double sepet_total()
        {
            return (double)urunler.Sum(i => i.TBLURUNLER.FIYAT * i.adet);
            
       
        }





    public void urunsil()
        {
            urunler.Clear();
        }
    
    
    }


    public class sepetherurun
    {
        public TBLURUNLER TBLURUNLER { get; set; }
        public int adet { get; set; }

    }




}