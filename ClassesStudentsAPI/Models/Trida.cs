using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesStudentsAPI.Models
{
    public class Trida
    {
        public int TridaId { get; set; }

        public string KodoveOznaceni { get; set; }

        [Required]
        public DateTime DatumVzniku { get; set; }

        [Required]
        public DateTime DatumUkonceni { get; set; }

        [Required]
        public UrovenVzdelani UrovenVzdelani { get; set; }

    }

    public enum UrovenVzdelani
    {
        ZŠ = 1,
        SŠ = 2,
        VŠ = 3
    }
}
