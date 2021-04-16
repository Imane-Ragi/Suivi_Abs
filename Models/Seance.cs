using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Suivi_Abs.Models
{
    public class Seance
    {
        [Key]
        public int id_S { get; set; }
        [Display(Name = "Date de la seance")]
       
        public DateTime date_S { get; set; }
        [Display(Name = "Numero de la seance")]
        public string Num_S { get; set; }
        public  string Duree { get; set; }
      

        [Display(Name = "Numero de la salle")]
        public string numSalle { get; set; }
        [Display(Name = "Matière")]
        public int id_M { get; set; }
        [Display(Name = "Groupe")]
        public int id_G { get; set; }
     
        [ForeignKey("id_M")]
        public  Matiere Matiere{ get; set; }
        [ForeignKey("id_G")]
        public  Groupe Groupe { get; set; }



    }
}
