using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Suivi_Abs.Models;

namespace Suivi_Abs.Controllers
{
    public class HomePController : Controller
    {
      
        private readonly ApplicationDbContext _context;



        public HomePController( ApplicationDbContext context)
        {

            _context = context;
        }
        // GET: HomeP
        public ActionResult Index(string statut)
        {
            ViewData["name"]= HttpContext.Session.GetString("name");
            ViewBag.Text = JsonConvert.SerializeObject(calculerTaux());
            ViewData["role"] = HttpContext.Session.GetString("role");
            ViewData["matricule"] = HttpContext.Session.GetString("matricule");

            var liste_abs = _context.absences.Include(m => m.Etudiant).Include(m => m.Seance).Include(m => m.Groupe).Include(m => m.Seance.Matiere).Where(x => x.Seance.Matiere.Professeur.matricule == ViewData["matricule"].ToString()).ToList();

            if (!String.IsNullOrEmpty(statut))
            {
                if (statut == "Tous")
                {
                    liste_abs = liste_abs.ToList();
                }
                else
                {
                    liste_abs = liste_abs.Where(x => x.Statut == statut).ToList();
                }
            }
            return View(liste_abs.ToList());
        }
      
        public async Task<ActionResult> Etudiant()
        {
            ViewData["matricule"] = HttpContext.Session.GetString("matricule");
            var etudiant = await _context.absences.Include(m => m.Etudiant.Groupe).Where(x => x.Seance.Matiere.Professeur.matricule == ViewData["matricule"].ToString() && x.Statut == "absent").ToListAsync();
            return View(etudiant);
         
        }
        public ActionResult Abs_par_etudiant( int ? id)
        {
            ViewData["matricule"] = HttpContext.Session.GetString("matricule");
            
            var etud = _context.absences.Include(m => m.Seance).Include(m => m.Groupe).Include(m => m.Seance.Matiere).Where(x => x.Seance.Matiere.Professeur.matricule == ViewData["matricule"].ToString() && x.Statut == "absent").Where(x => x.id_E == id).ToList();
            double absence = etud.Count();
            double totalSeance = _context.absences.Where(x => x.Seance.Matiere.Professeur.matricule == ViewData["matricule"].ToString()  && x.id_E == id).Count();
          
            double taux = absence / totalSeance * 100;
            ViewBag.nbrAbs = absence;
            ViewBag.taux = taux.ToString(".##");
        
            return View(etud);

        }
        public IQueryable calculerTaux()
        {
            ViewData["matricule"] = HttpContext.Session.GetString("matricule");
            var ttseance = (from m in _context.matieres
                            where m.Professeur.matricule== ViewData["matricule"].ToString()
                            join s in _context.seances on m.id_M equals s.id_M
                            join a in _context.absences on s.id_S equals a.id_S
                            group a by m.nMatiere into table
                            select new
                            {

                                name = table.Key,
                                y = table.Count()
                            });

            var seanceabs = (from m in _context.matieres
                             where m.Professeur.matricule == ViewData["matricule"].ToString()
                             join s in _context.seances on m.id_M equals s.id_M
                             join a in _context.absences on s.id_S equals a.id_S
                             where a.Statut == "absent"
                             group a by m.nMatiere into table
                             select new
                             {
                                 name = table.Key,
                                 y = table.Count() / ttseance.Where(x => x.name == table.Key).FirstOrDefault().y * 100
                             });
            return seanceabs;
        }

    }
}