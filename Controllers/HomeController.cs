using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Suivi_Abs.Models;

namespace Suivi_Abs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
      
        public IActionResult Index(string statut)
        {
            ViewBag.Text = JsonConvert.SerializeObject(calculerTaux());
            ViewData["name"] = HttpContext.Session.GetString("name");
            ViewData["role"] = HttpContext.Session.GetString("role");
            ViewBag.date = DateTime.Now.ToString();
            var liste_abs = _context.absences.Include(m => m.Etudiant).Include(m => m.Seance).Include(m => m.Groupe).Include(m => m.Seance.Matiere).ToList();

            if (!String.IsNullOrEmpty(statut))
            {
                if(statut=="Tous")
                {
                    liste_abs = liste_abs.ToList();
                }
                else
                {
                liste_abs = liste_abs.Where(x => x.Statut == statut).ToList();
                }
            }
            return View( liste_abs.ToList());
        }
    
     

            
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IQueryable calculerTaux()
        {
            var ttseance = (from m in _context.matieres
                            join s in _context.seances on m.id_M equals s.id_M
                            join a in _context.absences on s.id_S equals a.id_S
                            group a by m.nMatiere into table
                            select new
                            {

                                name = table.Key,
                                y = table.Count()
                            });

            var seanceabs = (from m in _context.matieres
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
