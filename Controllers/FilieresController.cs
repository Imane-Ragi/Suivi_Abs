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
    public class FilieresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilieresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filieres
        public async Task<IActionResult> Index()
        {
            return View(await _context.filieres.ToListAsync());
        }

        // GET: Filieres/Details/5
        public IActionResult AddorEdit(int id = 0)
        {

            if (id == 0)
            { return View(new Filiere()); }
            else
                return View(_context.filieres.Find(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_F", "nFiliere")]  Filiere filiere)
        {
           
            
            if (ModelState.IsValid)
            {
                if (filiere.id_F == 0)
                {

                    _context.Add(filiere);
                }
                else

                    _context.Update(filiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filiere);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var fil = await _context.filieres.FindAsync(id);
            _context.filieres.Remove(fil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
