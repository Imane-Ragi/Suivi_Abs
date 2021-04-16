using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
   
    
    public class Absence
    {
        

        [Key]
        public int id_RES { get; set; }

        public string Statut { get; set; }

        public int id_S { get; set; }
       
        public int id_E { get; set; }
      
        public int id_G { get; set; }
        [ForeignKey("id_E")]
        public  Etudiant Etudiant { get; set; }
        [ForeignKey("id_S")]
        public  Seance Seance { get; set; }
        [ForeignKey("id_G")]
        public  Groupe Groupe { get; set; }




    }
}
