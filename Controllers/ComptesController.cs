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
    public class ComptesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComptesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filieres
        public async Task<IActionResult> Index()
        {
            return View(await _context.comptes.ToListAsync());
        }

        // GET: Filieres/Details/5
        public IActionResult AddorEdit(int id = 0)
        {
            var listep = new SelectList(_context.professeurs.ToList(), "matricule", "matricule");
            ViewBag.prof = listep;

            if (id == 0)
            { return View(new Compte()); }
            else
                return View(_context.comptes.Find(id));

        }
        public IActionResult AddorEditA(int id = 0)
        {
            var listep = new SelectList(_context.professeurs.ToList(), "matricule", "matricule");
            ViewBag.prof = listep;

            if (id == 0)
            { return View(new Compte()); }
            else
                return View(_context.comptes.Find(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_C","email","password","matricule","nComplet","poste","roleC")] Compte compte)
        {
            
            
            var listep = new SelectList(_context.professeurs.ToList(), "matricule", "matricule");
           
            ViewBag.prof = listep;

             if (ModelState.IsValid)
             {
                      if (compte.id_C == 0)
                {
                       compte.roleC = "prof";
                        _context.Add(compte);
                }

            
            else
                {
                 
                    compte.roleC = "prof";
                   _context.Update(compte);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compte);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEditA([Bind("id_C", "email", "password", "matricule", "nComplet", "poste", "roleC")] Compte compte, string typeCpt)
        {
            var professeur = _context.professeurs;
            var listep = new SelectList(_context.professeurs.ToList(), "matricule", "matricule");
            ViewBag.prof = listep;

            if (ModelState.IsValid)
            {
                if (compte.id_C == 0)
                {

                    compte.roleC = "admin";
                    _context.Add(compte);
                }
                else
                {
                    compte.roleC = "admin";
                _context.Update(compte);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(compte);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var cpt = await _context.comptes.FindAsync(id);
            _context.comptes.Remove(cpt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
