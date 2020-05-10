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
    public class JurisdiccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jurisdicciones
        public ActionResult Index()
        {
            return View(db.Jurisdiccions.ToList());
        }

        // GET: Jurisdicciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jurisdiccion jurisdiccion = db.Jurisdiccions.Find(id);
            if (jurisdiccion == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccion);
        }

        // GET: Jurisdicciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jurisdicciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Jurisdiccion jurisdiccion)
        {
            if (ModelState.IsValid)
            {
                db.Jurisdiccions.Add(jurisdiccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jurisdiccion);
        }

        // GET: Jurisdicciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jurisdiccion jurisdiccion = db.Jurisdiccions.Find(id);
            if (jurisdiccion == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccion);
        }

        // POST: Jurisdicciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Jurisdiccion jurisdiccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jurisdiccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jurisdiccion);
        }

        // GET: Jurisdicciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jurisdiccion jurisdiccion = db.Jurisdiccions.Find(id);
            if (jurisdiccion == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccion);
        }

        // POST: Jurisdicciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jurisdiccion jurisdiccion = db.Jurisdiccions.Find(id);
            db.Jurisdiccions.Remove(jurisdiccion);
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
