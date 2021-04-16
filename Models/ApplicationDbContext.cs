using Microsoft.EntityFrameworkCore;
using Suivi_Abs.Models;


namespace Suivi_Abs.Models
{
    public class ApplicationDbContext : DbContext
    {
   

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Compte> comptes { get; set; }

        public DbSet<Etudiant> etudiants { get; set; }
        public DbSet<Professeur> professeurs { get; set; }
        public DbSet<Filiere> filieres { get; set; }
        public DbSet<Matiere> matieres { get; set; }
        public DbSet<Seance> seances { get; set; }
       
        public DbSet<Absence> absences { get; set; }

        public DbSet<Groupe> groupes { get; set; }

       

    }
}
