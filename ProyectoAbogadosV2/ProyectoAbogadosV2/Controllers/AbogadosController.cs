using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAbogadosV2.Models;

namespace ProyectoAbogadosV2.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AbogadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Abogados
        public ActionResult Index()
        {
            return View(db.Abogadoes.ToList());
        }

        // GET: Abogados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abogado abogado = db.Abogadoes.Find(id);
            if (abogado == null)
            {
                return HttpNotFound();
            }
            return View(abogado);
        }

        // GET: Abogados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Abogados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreAbogado,ApellidosAbogado,NifAbogado,MovilAbogado,FijoAbogado,Email,Direccion_Cliente,Poblacion_Cliente,Cp_Cliente")] Abogado abogado)
        {
            //En vez de coger el Mail del usuario logueado, lo obtengo del parametro de la vista anterior
            abogado.Email = Request.QueryString["user"];
            if (ModelState.IsValid)
            {
                db.Abogadoes.Add(abogado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(abogado);
        }

        // GET: Abogados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abogado abogado = db.Abogadoes.Find(id);
            if (abogado == null)
            {
                return HttpNotFound();
            }
            return View(abogado);
        }

        // POST: Abogados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreAbogado,ApellidosAbogado,NifAbogado,MovilAbogado,FijoAbogado,Email,Direccion_Cliente,Poblacion_Cliente,Cp_Cliente")] Abogado abogado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abogado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(abogado);
        }

        // GET: Abogados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abogado abogado = db.Abogadoes.Find(id);
            if (abogado == null)
            {
                return HttpNotFound();
            }
            return View(abogado);
        }

        // POST: Abogados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abogado abogado = db.Abogadoes.Find(id);
            db.Abogadoes.Remove(abogado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
