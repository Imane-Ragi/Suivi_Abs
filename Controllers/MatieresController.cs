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
    public class MatieresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatieresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matieres
        public async Task<IActionResult> Index()
        {
       
            var matiere = await _context.matieres.Include(m => m.filiere).Include(n=>n.Professeur).ToListAsync();
            return View( matiere);
        }



        // GET: Matieres/Create
        public IActionResult AddorEdit(int id = 0)
        {
            
            var listef = new SelectList(_context.filieres.ToList(), "id_F", "nFiliere");
            ViewBag.liste = listef;
            var listep = new SelectList(_context.professeurs.ToList(), "id_P", "nComplet");
            ViewBag.prof = listep;
            if (id == 0)
            { return View(new Matiere()); }
            else
                return View(_context.matieres.Find(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_M,nMatiere,id_F,id_P")] Matiere matiere)
        {
            var listef = new SelectList(_context.filieres.ToList(), "id_F", "nFiliere");
            ViewBag.liste = listef;
            var listep = new SelectList(_context.professeurs.ToList(), "id_P", "nComplet");
            ViewBag.prof = listep;
            var search = _context.matieres.Where(x => x.id_F == matiere.id_F && x.nMatiere == matiere.nMatiere).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (matiere.id_M == 0)
                {
                   if(search !=null)

                    {
                        ViewBag.erreur = "Cette matiere existe déjà dans cette filière"; }
                    else 
                    { 
                        _context.Add(matiere);
                    }
                    
                }
                else
                
                _context.Update(matiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matiere);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var mat = await _context.matieres.FindAsync(id);
            _context.matieres.Remove(mat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





    }
}
