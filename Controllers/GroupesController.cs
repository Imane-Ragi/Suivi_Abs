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
    public class GroupesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groupes
        public async Task<IActionResult> Index()

        {
            var liste = await _context.groupes.Include(c => c.Filiere).ToListAsync();
            return View(liste);
        }

       
      
        public IActionResult AddorEdit(int id = 0)
        {
            var listef = new SelectList(_context.filieres.ToList(), "id_F", "nFiliere");
            ViewBag.liste = listef;
            if (id == 0)
            { return View(new Groupe( )); }
            else
                return View(_context.groupes.Find(id));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_G,Numgroupe,id_F")] Groupe groupe)
        {

            var listef = new SelectList(_context.filieres, "id_F", "nFiliere", groupe.id_F);
            ViewBag.liste = listef;
            if (ModelState.IsValid)
            {
                if (groupe.id_G == 0)
                {
                   
                   
                    _context.Add(groupe);
                 
                }
                else
                   
                _context.Update(groupe);
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(groupe);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var grp = await _context.groupes.FindAsync(id);
            _context.groupes.Remove(grp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListEtudiants(int? id)

        {
            Groupe groupe = _context.groupes.Find(id);

            return View(_context.etudiants.Where(m => m.id_G ==  groupe.id_G).ToList());

        }

    }
}
