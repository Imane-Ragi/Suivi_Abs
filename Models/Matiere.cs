using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Matiere
    {
        [Key]
        public int id_M { get; set; }
        [Display(Name = "Matiere")]

        public string nMatiere { get; set; }


        [Display(Name = "Professeur")]
        public int id_P { get; set; }
        [Display(Name = "Filiere")]
        public int id_F { get; set; }
        [ForeignKey("id_F")]
        public  Filiere filiere { get; set; }
        [ForeignKey("id_P")]
        public Professeur Professeur { get; set; }
    }
}
