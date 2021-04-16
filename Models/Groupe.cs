using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Groupe
    {
        [Key]
        public int id_G { get; set; }
        public string Numgroupe { get; set; }
        [Display(Name = "Filière")]
        public int id_F { get; set; }
        [ForeignKey("id_F")]
        public  Filiere Filiere { get; set; }
       



      

    }
}
