using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesStudentsAPI.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string Jmeno { get; set; }

        [Required]
        public string Prijmeni { get; set; }

        [Required]
        [RegularExpression("[0-9]{9,9}$")]
        public string TelefonniCislo { get; set; }

        [Required]
        public Pohlavi Pohlavi { get; set; }

        [Required]
        public DateTime DatumNarozeni { get; set; }

        public int TridaId { get; set; }
    }

    public enum Pohlavi
    {
        Muž = -1,
        Žena = 1
    }
}
