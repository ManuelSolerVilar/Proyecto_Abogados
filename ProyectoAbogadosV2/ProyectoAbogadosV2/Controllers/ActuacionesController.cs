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
    public class ActuacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actuaciones
        public ActionResult Index()
        {
            var actuacions = db.Actuacions.Include(a => a.Expediente);
            return View(actuacions.ToList());
        }

        // GET: Actuaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuacion actuacion = db.Actuacions.Find(id);
            if (actuacion == null)
            {
                return HttpNotFound();
            }
            return View(actuacion);
        }

        // GET: Actuaciones/Create
        public ActionResult Create()
        {
            ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente");
            return View();
        }

        // POST: Actuaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpedienteId,FechaInicio,FechaCierre,Descripcion,NotificacionCliente,NotificacionJuzgado")] Actuacion actuacion)
        {
            if (ModelState.IsValid)
            {
                db.Actuacions.Add(actuacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente", actuacion.ExpedienteId);
            return View(actuacion);
        }

        // GET: Actuaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuacion actuacion = db.Actuacions.Find(id);
            if (actuacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente", actuacion.ExpedienteId);
            return View(actuacion);
        }

        // POST: Actuaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpedienteId,FechaInicio,FechaCierre,Descripcion,NotificacionCliente,NotificacionJuzgado")] Actuacion actuacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actuacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente", actuacion.ExpedienteId);
            return View(actuacion);
        }

        // GET: Actuaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuacion actuacion = db.Actuacions.Find(id);
            if (actuacion == null)
            {
                return HttpNotFound();
            }
            return View(actuacion);
        }

        // POST: Actuaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actuacion actuacion = db.Actuacions.Find(id);
            db.Actuacions.Remove(actuacion);
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
