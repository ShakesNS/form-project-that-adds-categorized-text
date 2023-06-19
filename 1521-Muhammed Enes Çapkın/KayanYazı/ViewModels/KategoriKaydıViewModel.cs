using KayanYazı.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayanYazı.ViewModels
{
    public class KategoriKaydıViewModel : ViewModel
    {
        Kategori Kategori;

        public KategoriKaydıViewModel()
        {

        }

        public KategoriKaydıViewModel(Kategori kategori)
        {
            Kategori = kategori;
            
        }


        private string kategoriadı;
        [Required]
        public string KategoriAdı
        {
            get { return kategoriadı; }
            set { kategoriadı = value; NotifyPropertyChanged(); }
        }



        void KategoriKaydet()
        {
            if (Kategori == null)
            {
                Kategori = new Kategori();
                Veriler.Db.Kategoriler.Add(Kategori);

            }
            Kategori.KategoriAdı = kategoriadı;
            Veriler.Db.SaveChanges();

            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.Frm.Navigate(new Views.ListelePage());




        }

        private ActionCommand kategorikaydetCommand;
        public ActionCommand KategoriKaydetCommand
        {
            get { return kategorikaydetCommand ?? (kategorikaydetCommand = new ActionCommand(p => KategoriKaydet(), p => IsValid)); }
        }
    }
}
