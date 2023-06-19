using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayanYazı.Models
{
    [Table(nameof(DbModel.Kategoriler))]
    public class Kategori
    {
        public int Id { get; set; }

        public string KategoriAdı { get; set; }

        public virtual ICollection<Metin> Metinler { get; set; } = new HashSet<Metin>();

        public override string ToString()
        {
            return KategoriAdı;
        }



    }
}
