using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ProyectoAbogadosV2.Models;

namespace ProyectoAbogadosV2.Controllers
{
    

    public class ExpedientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expedientes
        public ActionResult Index(string strTipoJurisdiccion, string strCadenaBusqueda, int? page,
            string StrBusquedaActual, string strFiltroActual)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                // Para mostrar la primera página cuando se ha introducido una cadena de búsqueda
                if (strCadenaBusqueda != null)
                {
                    page = 1;
                }
                else
                {
                    strCadenaBusqueda = StrBusquedaActual;
                }
                ViewBag.BusquedaActual = strCadenaBusqueda;
                // Para mostrar la primera página cuando se ha cambiado la selección en el DropDownList
                if (strTipoJurisdiccion != null)
                {
                    page = 1;
                }
                else
                {
                    strTipoJurisdiccion = strFiltroActual;
                }
                ViewBag.FiltroActual = strTipoJurisdiccion;

                var expedientes = db.Expedientes.Include(e => e.Abogado).Include(e => e.Cliente).Include(e => e.Jurisdiccion);

                //Para Ordenar por fecha de inicio del expediente
                expedientes = expedientes.OrderByDescending(s => s.FechaInicio);

                // Para presentar los tipos de Jurisdicciones en la vista
                var lstTipoJurisdiccion = new List<string>();
                var qryTipoJurisdiccion = from d in db.Expedientes
                                          orderby d.Jurisdiccion.Nombre
                                          select d.Jurisdiccion.Nombre;
                lstTipoJurisdiccion.Add("Todas");
                lstTipoJurisdiccion.AddRange(qryTipoJurisdiccion.Distinct());
                ViewBag.ListaTipoAverias = new SelectList(lstTipoJurisdiccion);

                

                if (User.IsInRole("Administrador"))//Si soy un abogado (el administrador es un abogado)
                {
                    if (!(User.Identity.Name == "admin@empresa.com"))//Si no soy el administrador tiene el correo "admin@empresa"
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

                if (!String.IsNullOrEmpty(strCadenaBusqueda))
                {
                    expedientes = expedientes.Where(s => s.Cliente.Nombre.Contains(strCadenaBusqueda));
                }
                if (!string.IsNullOrEmpty(strTipoJurisdiccion))
                {
                    if (strTipoJurisdiccion != "Todas")
                    {
                        expedientes = expedientes.Where(x => x.Jurisdiccion.Nombre == strTipoJurisdiccion);
                    }
                }
                //Características de la paginación
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(expedientes.ToPagedList(pageNumber, pageSize));

                //return View(expedientes.ToList());
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Expedientes/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }


        // GET: Expedientes/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                ViewBag.AbogadoId = new SelectList(db.Abogadoes, "Id", "NombreAbogado");
                ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre");
                ViewBag.JurisdiccionId = new SelectList(db.Jurisdiccions, "Id", "Nombre");
                return View();
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Expedientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TituloExpediente,FechaInicio,FechaCierre,Descripcion,AbogadoId,ClienteId,JurisdiccionId,ProvisionFondos,TotalMinuta")] Expediente expediente)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Expedientes/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Expedientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TituloExpediente,FechaInicio,FechaCierre,Descripcion,AbogadoId,ClienteId,JurisdiccionId,ProvisionFondos,TotalMinuta")] Expediente expediente)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: Expedientes/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
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
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }
        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            foreach (Actuacion actu in db.Actuacions.Where(a => a.ExpedienteId == id))//Borrar todos los documentos relacionados con una actuacion
            {
                ActuacionesController ac = new ActuacionesController();
                ac.DeleteCascade(actu.Id);
            }
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
