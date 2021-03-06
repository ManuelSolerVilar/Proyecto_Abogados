﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoAbogadosV2.Models;

namespace ProyectoAbogadosV2.Controllers
{
    public class ActuacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actuaciones
        public ActionResult Index()
        {
            /* Intentar sacar el rol si no cuando meta abogados 
             * como administradores va a petar. if (!(User.Identity.Name == "admin@empresa.com"))//CAMBIAR
            {
                Cliente cliente = db.Clientes.Where(c => c.Email == User.Identity.Name).FirstOrDefault();
                var actuacions = actuaciones.Where(s => s.ClienteId == cliente.Id);
            }
            // Se seleccionan los datos del empleado correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            if ((User.Identity.Name == "admin@empresa.com"))//CAMBIAR
            {*/
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                var actuacions = db.Actuacions.Include(a => a.Expediente);

                //Para filtrar las actuaciones por aquellas que pertenecen a un expediente
                var expedientes = db.Expedientes.Include(e => e.Abogado).Include(e => e.Cliente).Include(e => e.Jurisdiccion);

                if (User.IsInRole("Administrador"))//Si soy un abogado (el administrador es un abogado)
                {
                    if (!(User.Identity.Name == "admin@empresa.com"))//Si no soy el administrador
                    {
                        Abogado abogado = db.Abogadoes.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                        expedientes = expedientes.Where(s => s.AbogadoId == abogado.Id);
                    }
                }
                else//Si soy un cliente
                {
                    Cliente cliente = db.Clientes.Where(c => c.Email == User.Identity.Name).FirstOrDefault();
                    expedientes = expedientes.Where(s => s.ClienteId == cliente.Id);
                }

                actuacions = actuacions.Where(a => expedientes.Any(e=>e.Id==a.ExpedienteId));

                return View(actuacions.ToList());
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
            /*}
            else 
            {
                Cliente cliente = db.Clientes.Where(c => c.Email == User.Identity.Name).FirstOrDefault();
                var actuacions = expedientes.Where(s => s.ClienteId == cliente.Id);
                var actuacions = db.Actuacions.Include()
                return View(actuacions.ToList());
            }*/

        }

        // GET: Actuaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: Actuaciones/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente");
                return View();
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Actuaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpedienteId,FechaInicio,FechaCierre,Descripcion,NotificacionCliente,NotificacionJuzgado")] Actuacion actuacion)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                actuacion.ExpedienteId = int.Parse(Request.QueryString["idExpediente"]);
                if (ModelState.IsValid)
                {
                    db.Actuacions.Add(actuacion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ExpedienteId = new SelectList(db.Expedientes, "Id", "TituloExpediente", actuacion.ExpedienteId);
                return View(actuacion);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: Actuaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // POST: Actuaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpedienteId,FechaInicio,FechaCierre,Descripcion,NotificacionCliente,NotificacionJuzgado")] Actuacion actuacion)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

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
            DeleteCascade(id);
            return RedirectToAction("Index");
        }
        public void DeleteCascade(int id)
        {
            foreach (Documento doc in db.Documentoes.Where(d => d.Actuacion.Id == id))//Borrar todos los documentos relacionados con una actuacion
            {
                DocumentosController dc = new DocumentosController();
                dc.DeleteCascade(doc.Id);
            }
            Actuacion actuacion = db.Actuacions.Find(id);
            db.Actuacions.Remove(actuacion);
            db.SaveChangesAsync();
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
