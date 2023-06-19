using KayanYazı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KayanYazı.ViewModels
{
    public class ListeleViewModel : ViewModel
    {
        public Kategori[] Kategoriler { get { return Veriler.Db.Kategoriler.ToArray(); } }
        public Metin[] Metinler
        {
            get { return Veriler.Db.Metinler.ToArray(); }
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

        void KategoriSil()
        {
            if (kategori != null)
            {
                var cevap = MessageBox.Show("Seçili kişi silinecek silinsin mi?", "Sil", MessageBoxButton.YesNoCancel, MessageBoxImage.Stop);
                if (cevap == MessageBoxResult.Yes)
                {
                    Veriler.Db.Kategoriler.Remove(kategori);
                    Veriler.Db.SaveChanges();
                    NotifyPropertyChanged(nameof(Kategoriler));
                }
            }
        }

        private ActionCommand kategorisilCommand;
        public ActionCommand KategoriSilCommand
        {
            get { return kategorisilCommand ?? (kategorisilCommand = new ActionCommand(p => KategoriSil(), p => IsValid)); }
        }

        void MetinSil()
        {
            if (metin != null)
            {
                var cevap = MessageBox.Show("Seçili kişi silinecek silinsin mi?", "Sil", MessageBoxButton.YesNoCancel, MessageBoxImage.Stop);
                if (cevap == MessageBoxResult.Yes)
                {
                    Veriler.Db.Metinler.Remove(metin);
                    Veriler.Db.SaveChanges();
                    NotifyPropertyChanged(nameof(Metinler));
                }
            }
        }

        private ActionCommand metinsilCommand;
        public ActionCommand MetinSilCommand
        {
            get { return metinsilCommand ?? (metinsilCommand = new ActionCommand(p => MetinSil(), p => IsValid)); }
        }
    }
}
