using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Suivi_Abs.Models;

namespace Suivi_Abs.Controllers
{
    public class EtudiantsController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EtudiantsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;

            _context = context;
        }

        // GET: Professeurs
        public async Task<IActionResult> Index()
        {
            var etud = await _context.etudiants.Include(m => m.Groupe ).ToListAsync();
            return View(etud);
        }

       

        // GET: Professeurs/Create
        public IActionResult AddorEdit(int id = 0)
        {
            var listef = new SelectList(_context.groupes.ToList(), "id_G", "Numgroupe");
            ViewBag.liste = listef;
            if (id == 0)
            { return View(new Etudiant()); }
            else
                TempData["image"] = _context.etudiants.Find(id).imageE;
                return View(_context.etudiants.Find(id));

        }

        // POST: Professeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id_E,matricule,Groupe,Ncomplet, dateN ,lieu_n,adresse,imageE ,email,id_G")] Etudiant etudiant)
        {
            var listef = new SelectList(_context.groupes.ToList(), "id_G", "Numgroupe",etudiant.id_G);
            ViewBag.liste = listef;
         
            string uniqueFileName = UploadedFile(etudiant);
          

            if (ModelState.IsValid)
            {
                if (etudiant.id_E == 0)
                {
                   
                    etudiant.imageE = uniqueFileName;
                    _context.Add(etudiant);
                }
                else
                {
                    if (etudiant.imageE != null)
                    {
                        etudiant.imageE = uniqueFileName;
                    }
                    else
                        etudiant.imageE = Convert.ToString(TempData["image"]);
                       _context.Update(etudiant);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etudiant);
        }

        private string UploadedFile(Etudiant e)
        {

            if (e.file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

             
                e.imageE = Guid.NewGuid().ToString() + "_" +uploadsFolder+ e.file.FileName;
                string filePath = Path.Combine(uploadsFolder, e.imageE);
                e.file.CopyTo(new FileStream(filePath, FileMode.Create));
                
                 
                
            }
            return e.imageE;
        }



        public async Task<IActionResult> Delete(int? id)
        {
            var etud = await _context.etudiants.FindAsync(id);
            _context.etudiants.Remove(etud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Affichage des liste d'absence de chaque etudiants avec le nombre d'absences et le taux d'absentiesmes 

        public IActionResult ListAbs(int? id)
        {
            double absence = _context.absences.Where(a => a.id_E == id && a.Statut == "absent").Count();
            double totalSeance = _context.absences.Where(a => a.id_E == id).Count();
            double taux = absence / totalSeance * 100;
            ViewBag.nbrAbs = _context.absences.Where(a => a.id_E == id && a.Statut == "absent").Count();
            ViewBag.taux = taux.ToString(".##");
            return View(_context.absences.Include(m => m.Seance).Include(m => m.Seance.Matiere).Where(a => a.id_E == id && a.Statut == "absent").ToList());
        }
    }
}
