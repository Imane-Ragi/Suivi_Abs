using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Suivi_Abs.Models;

namespace Suivi_Abs.Controllers
{
    public class SeancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seances
        public async Task<IActionResult> Index()
        {
            var seance = await _context.seances.Include(m => m.Matiere.Professeur).Include(s=>s.Groupe.Filiere).ToListAsync();
            return View(seance);
        }


        public IActionResult AddorEdit(int id = 0)
        {
            
            var listem = new SelectList(_context.matieres.ToList(), "id_M", "nMatiere");
            ViewBag.mat = listem;
            var listeg = new SelectList(_context.groupes.ToList(), "id_G", "Numgroupe");
            ViewBag.group = listeg;
           

            if (id == 0)
            { return View(new Seance()); }
            else
             
            return View(_context.seances.Find(id));

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_S,date_S,Num_S,Duree,numSalle,id_M,id_G")] Seance seance)
        {
          
            var listem = new SelectList(_context.matieres.ToList(), "id_M", "nMatiere",seance.id_M);
            ViewBag.mat = listem;
            var listeg = new SelectList(_context.groupes.ToList(), "id_G", "Numgroupe",seance.id_G);
            ViewBag.group = listeg;


            if (ModelState.IsValid)
            {
                if (seance.id_S == 0)
                { 


                    _context.Add(seance);
                    await _context.SaveChangesAsync();
                    AddlistAbs(seance);
                }
                else

                    _context.Update(seance);
                await _context.SaveChangesAsync();
                updatelistAbs(seance);
                return RedirectToAction(nameof(Index));
            }
            return View(seance);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var sea = await _context.seances.FindAsync(id);
            _context.seances.Remove(sea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public void AddlistAbs(Seance seance)
        {
            var etudiants = _context.etudiants.Where(s => s.id_G == seance.id_G).ToList();
            foreach (Etudiant e in etudiants)
            {
                Absence abs = new Absence();
                abs.id_E = e.id_E;
                abs.id_G = seance.id_G;
                abs.id_S = seance.id_S;
                abs.Statut = "Absent";
                _context.absences.Add(abs);
                _context.SaveChanges();
            }

           
        }
        public void updatelistAbs(Seance seance)
        {
            var absences = _context.absences.Where(s => s.id_S == seance.id_S).ToList();
            foreach (Absence abs in absences)
            {
                Absence abs_courante = _context.absences.Find(abs.id_RES);

                abs_courante.id_E = abs.id_E;
                abs_courante.id_G = seance.id_G;
                abs_courante.id_S = seance.id_S;
                _context.absences.Update(abs_courante);
                _context.SaveChanges();
            }


        }
       

    }
}
