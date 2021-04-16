using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
  
    public class Filiere
    {
        [Key]
        public int id_F { get; set; }
        [Display(Name = "Filiere")]
        public string nFiliere { get; set; }
    }
}
