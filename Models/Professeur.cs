
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Professeur 
    {
        [Key]
        public int id_P { get; set; }
        [Required (ErrorMessage ="entrez votre matricule")]
        [Display(Name = "Matricule")]
        public string matricule { get; set; }
        [Display(Name = "Nom Complet")]
        public string nComplet { get; set; }
        [Display(Name = "Specialité")]
        public string specialité { get; set; }
        [Display(Name = "Date Naissance")]

        [DataType(DataType.Date)]

        public DateTime dateN { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Recrutement")]
        public DateTime  dateR { get; set; }
        public string email { get; set; }
      
      

     



    }
}
