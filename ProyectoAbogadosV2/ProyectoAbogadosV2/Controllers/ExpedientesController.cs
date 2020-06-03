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
    

    public class ExpedientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expedientes
        public ActionResult Index(string strTipoJurisdiccion, string strCadenaBusqueda)
        {
            var expedientes = db.Expedientes.Include(e => e.Abogado).Include(e => e.Cliente).Include(e => e.Jurisdiccion);
            expedientes = expedientes.OrderByDescending(s => s.FechaInicio); //Para Ordenar por fecha de inicio del expediente

            // Para presentar los tipos de avería en la vista
            var lstTipoJurisdiccion = new List<string>();
            var qryTipoJurisdiccion = from d in db.Expedientes
                                      orderby d.Jurisdiccion.Nombre
                                      select d.Jurisdiccion.Nombre;
            lstTipoJurisdiccion.AddRange(qryTipoJurisdiccion.Distinct());
            ViewBag.ListaTipoAverias = new SelectList(lstTipoJurisdiccion);

            if (!String.IsNullOrEmpty(strCadenaBusqueda))
            {
                expedientes = expedientes.Where(s => s.Cliente.Nombre.Contains(strCadenaBusqueda));
            }
            if (!string.IsNullOrEmpty(strTipoJurisdiccion))
            {
                expedientes = expedientes.Where(x => x.Jurisdiccion.Nombre == strTipoJurisdiccion);
            }

            return View(expedientes.ToList());
        }

        // GET: Expedientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expediente expediente = db.Expedientes.Find(id);
            if (expediente == null)
            {
                return HttpNotFound();
            }
            return View(expediente);
        }


        // GET: Expedientes/Create
        [Authorize(Roles = "Administrador")]

        public ActionResult Create()
        {
            ViewBag.AbogadoId = new SelectList(db.Abogadoes, "Id", "NombreAbogado");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.JurisdiccionId = new SelectList(db.Jurisdiccions, "Id", "Nombre");
            return View();
        }

        // POST: Expedientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TituloExpediente,FechaInicio,FechaCierre,Descripcion,AbogadoId,ClienteId,JurisdiccionId,ProvisionFondos,TotalMinuta")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                db.Expedientes.Add(expediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AbogadoId = new SelectList(db.Abogadoes, "Id", "NombreAbogado", expediente.AbogadoId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", expediente.ClienteId);
            ViewBag.JurisdiccionId = new SelectList(db.Jurisdiccions, "Id", "Nombre", expediente.JurisdiccionId);
            return View(expediente);
        }

        // GET: Expedientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expediente expediente = db.Expedientes.Find(id);
            if (expediente == null)
            {
                return HttpNotFound();
            }
            ViewBag.AbogadoId = new SelectList(db.Abogadoes, "Id", "NombreAbogado", expediente.AbogadoId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", expediente.ClienteId);
            ViewBag.JurisdiccionId = new SelectList(db.Jurisdiccions, "Id", "Nombre", expediente.JurisdiccionId);
            return View(expediente);
        }

        // POST: Expedientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TituloExpediente,FechaInicio,FechaCierre,Descripcion,AbogadoId,ClienteId,JurisdiccionId,ProvisionFondos,TotalMinuta")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AbogadoId = new SelectList(db.Abogadoes, "Id", "NombreAbogado", expediente.AbogadoId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", expediente.ClienteId);
            ViewBag.JurisdiccionId = new SelectList(db.Jurisdiccions, "Id", "Nombre", expediente.JurisdiccionId);
            return View(expediente);
        }

        // GET: Expedientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expediente expediente = db.Expedientes.Find(id);
            if (expediente == null)
            {
                return HttpNotFound();
            }
            return View(expediente);
        }
        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expediente expediente = db.Expedientes.Find(id);
            db.Expedientes.Remove(expediente);
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
