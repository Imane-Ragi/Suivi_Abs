using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Etudiant 
    {
        [Key]
        public int id_E { get; set; }
        [Display(Name = "Matricule")]
        [Required(ErrorMessage = "entrez la matricule")]
        public string matricule { get; set; }
   
        [Display(Name = "Nom Complet")]
        public string Ncomplet { get; set; }
      
        [Display(Name = "Date de Naissance")]
        public DateTime dateN { get; set; }
        [Display(Name = "Lieu de Naissance")]
        public string lieu_n { get; set; }
        [Display(Name = "Adresse")]
        public string adresse { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        [Display(Name = "Photo")]
        public string imageE { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Groupe")]
        public int id_G { get; set; }
        [ForeignKey("id_G")]
        public Groupe Groupe { get; set; }


    }
}
