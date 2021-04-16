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
    public class ProfesseursController : Controller
    {
        private readonly ApplicationDbContext _context;
    

        public ProfesseursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Professeurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.professeurs.ToListAsync());
        }

     

        // GET: Professeurs/Create
        public IActionResult AddorEdit(int id=0)
        {
            if (id == 0)
            { return View(new Professeur()); }
            else
                return View(_context.professeurs.Find(id));
             
        }

        // POST: Professeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_P,matricule,nComplet,specialité,dateN,dateR")] Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                if (professeur.id_P == 0)
                {
                    _context.Add(professeur);
                }
                else
                    _context.Update(professeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professeur);
        }

      

      
      
        public async Task<IActionResult> Delete(int? id)
        {
            var prof = await _context.professeurs.FindAsync(id);
            _context.professeurs.Remove(prof);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
     
    }
}
