
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Compte 
    {
        [Key]
        public int id_C { get; set; }
        [Required(ErrorMessage = "veuillez entrez votre email")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(10, ErrorMessage = "le mot de passe doit contenir au moins 6 caractères "), MinLength(6)]
        [Required(ErrorMessage = "veuillez entrez votre mot de passe")]
        public string password { get; set; }
        [Required(ErrorMessage = "veuillez entrez votre matricule")]
        public string matricule { get; set; }
        
        public string nComplet { get; set; }
     
        public string poste { get; set; }

        public string roleC { get; set; }

    
    }
}
