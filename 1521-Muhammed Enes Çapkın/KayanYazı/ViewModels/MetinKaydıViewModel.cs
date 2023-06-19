using KayanYazı.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayanYazı.ViewModels
{
    public class MetinKaydıViewModel : ViewModel
    {
        Metin Metin;

        public Kategori[] Kategoriler { get { return Veriler.Db.Kategoriler.ToArray(); } }

        public MetinKaydıViewModel()
        {

        }

        public MetinKaydıViewModel(Metin metin)
        {
            Metin = metin;

        }


        private string gerçekmetin;
        [Required]
        public string GerçekMetin
        {
            get { return gerçekmetin; }
            set { gerçekmetin = value; NotifyPropertyChanged(); }
        }

        private Kategori kategori;
        [Required]
        public Kategori Kategori
        {
            get { return kategori; }
            set { kategori = value; NotifyPropertyChanged(); }
        }



        void MetinKaydet()
        {
            if (Metin == null)
            {
                Metin = new Metin();
                Veriler.Db.Metinler.Add(Metin);

            }
            Metin.Kategori = kategori;
            Metin.GerçekMetin = gerçekmetin;
            Veriler.Db.SaveChanges();

            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.Frm.Navigate(new Views.ListelePage());




        }

        private ActionCommand metinkaydetCommand;
        public ActionCommand MetinKaydetCommand
        {
            get { return metinkaydetCommand ?? (metinkaydetCommand = new ActionCommand(p => MetinKaydet(), p => IsValid)); }
        }
    }
}
