using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayanYazı.Models
{
    [Table(nameof(DbModel.Metinler))]
    public class Metin
    {
        public int Id { get; set; }

        public string GerçekMetin { get; set; }

        [ForeignKey(nameof(Kategori))]
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
    }
}
