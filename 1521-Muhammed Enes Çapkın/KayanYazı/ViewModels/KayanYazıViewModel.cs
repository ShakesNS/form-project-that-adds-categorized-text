using KayanYazı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayanYazı.ViewModels
{
    public class KayanYazıViewModel : ViewModel
    {
        
        public Kategori[] Kategoriler { get { return Veriler.Db.Kategoriler.ToArray(); } }
        public Metin[] Metinler { get { return metin == null ? Veriler.Db.Metinler.ToArray() : metin.Kategori.Metinler.ToArray(); } }


        public KayanYazıViewModel()
        {
            string cümle = "";
            
            
        }

        private int gelenmetin;

        public int GelenMetin
        {
            get { return gelenmetin; }
            set { gelenmetin = value; NotifyPropertyChanged(); }
        }


        private Kategori kategori;
        public Kategori Kategori
        {
            get { return kategori; }
            set
            {
                kategori = value;
                NotifyPropertyChanged();

            }
        }

        private Metin metin;
        public Metin Metin
        {
            get { return metin; }
            set
            {
                metin = value;
                NotifyPropertyChanged();

            }
        }
    }
}
