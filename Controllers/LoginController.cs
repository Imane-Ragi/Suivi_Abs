using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suivi_Abs.Models;

namespace Suivi_Abs.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
       

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(dynamic error)
        {
           

            return View();
        }

        [HttpPost]
        public IActionResult CheckAccount(Compte cpt)
        {

            var Account = _context.comptes.Where(x => x.email == cpt.email && x.password == cpt.password).SingleOrDefault();
            if (Account == null)
            {

                ViewBag.error = "Compte introuvable veuillez contactez  l'administrateur ";
                return View("Index");
              




            }
            else
            {

               
                HttpContext.Session.SetString("matricule", Account.matricule.ToString());
                HttpContext.Session.SetString("role", Account.roleC.ToString());

                if (Account.roleC == "admin")
                { return RedirectToAction("Index", "Home"); }
                else { return RedirectToAction("Index", "HomeP"); }
              

            }


        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        public IActionResult Verifier()
        {

            return View();
        }
        /*  Cette  methode  est dedié  pour le pointage  des etudiants , l'etudiant doit entrez  sa matricule est  la date de la seance  apres  la 
         *  methode verifie  si l'etudiant existe apres si la seancce existe  sinon il va  afficher una message d'erreur selon le cas  */
        [HttpPost]
        public IActionResult VerifierAbs(string code, DateTime debut)
        {
            var date_aujourdhui = DateTime.Now;
         
            if (!String.IsNullOrEmpty(code))
            {
                var rechercher = _context.etudiants.Where(s => s.matricule.Equals(code)).FirstOrDefault();

                 if (rechercher == null)
                 {
                    ViewBag.vide = "Cet etudiant est introuvable  ";
                    return View("Verifier", code);

                 }

                 else
                 {
                    
                        var seance = _context.seances.Where(s => s.id_G == rechercher.id_G && s.date_S==debut  ).FirstOrDefault();
                    if (seance == null) { ViewBag.vide = "Seance Introuvable  "; return View("Verifier", code); }
                    else {
                        var date_fin = seance.date_S.AddHours(double.Parse(seance.Duree));
                        if (seance.date_S.ToShortDateString() == date_aujourdhui.ToShortDateString())
                        { 
                            if (   date_aujourdhui.Hour>= seance.date_S.Hour && date_aujourdhui.Hour < date_fin.Hour)
                                  {
                                   var abs = _context.absences.Where(b => b.id_E == rechercher.id_E && b.id_S == seance.id_S).FirstOrDefault();
                                   abs.Statut = "present";
                                   _context.absences.Update(abs);
                                  _context.SaveChanges();
                                  ViewBag.succes = "Successful ";
                                   }
 
                            else
                            {
                                ViewBag.echoue = "Trop tard ";

                            }
                        }
                        else
                        {
                            ViewBag.echoue = " la seance ne correspond pas aujourd'hui ";

                        }
                    }

                 }

            }
            else
            {
                ViewBag.vide = "remplir code ";

            }
            return View("Verifier", code);
        }


    }
}