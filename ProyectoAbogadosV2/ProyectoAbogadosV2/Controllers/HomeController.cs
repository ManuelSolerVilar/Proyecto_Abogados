using Microsoft.AspNet.Identity;
using ProyectoAbogadosV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAbogadosV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            // Si existe el empleado correspondiente al usuario actual
            // se va a View, en caso contrario se va a crear el empleado.
            string usuario = User.Identity.GetUserName();
            var cliente = db.Clientes.Where(e => e.Email == usuario).FirstOrDefault();
            if (User.Identity.IsAuthenticated &&
            User.IsInRole("Usuario") &&
            cliente == null)
            {
                return RedirectToAction("Create", "Clientes");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}