using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAbogadosV2.Models;

namespace ProyectoAbogadosV2.Controllers
{
    public class DocumentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documentos
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                var expedientes = new List<string>();
                var qryExpedientes = from d in db.Expedientes
                                     orderby d.Id
                                     select d.TituloExpediente;
                expedientes.AddRange(qryExpedientes.Distinct());
                ViewBag.ListaTipoAverias = new SelectList(expedientes);
                return View(db.Documentoes.ToList());
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Documentos/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Documento documento = db.Documentoes.Find(id);
                if (documento == null)
                {
                    return HttpNotFound();
                }
                return View(documento);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Documentos/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                return View();
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Documentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpedienteId,Descripcion,Documentacion")] Documento documento)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                if (ModelState.IsValid)
                {
                    db.Documentoes.Add(documento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(documento);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: Documentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Documento documento = db.Documentoes.Find(id);
                if (documento == null)
                {
                    return HttpNotFound();
                }
                return View(documento);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Documentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpedienteId,Descripcion,Documentacion")] Documento documento)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                if (ModelState.IsValid)
                {
                    db.Entry(documento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(documento);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Documentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Documento documento = db.Documentoes.Find(id);
                if (documento == null)
                {
                    return HttpNotFound();
                }
                return View(documento);
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Documentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteCascade(id);
            return RedirectToAction("Index");
        }

        public void DeleteCascade(int id)
        {
            Documento documento = db.Documentoes.Find(id);
            System.IO.File.Delete(documento.Documentacion);//Elimina el fichero del directorio actual.
            db.Documentoes.Remove(documento);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //GET SubirArchivo
        public ActionResult SubirArchivo()
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                return View();
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }



        }
        //POST SubirArchivo
        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)//Si no estas autenticado no puedes hacer nada
            {
                SubirArchivosModelo modelo = new SubirArchivosModelo();
                //Si se ha seleccionado un fichero para subir entra si no salta
              
                if (file != null)
                {
                    //Me creo una variable string y le digo a la ruta que quiero meter el fichero.
                    String ruta = Server.MapPath("../Temp/");
                    //Creo una variable y le extraigo al fichero introducido su nombre
                    String nombre = System.IO.Path.GetFileNameWithoutExtension(ruta + file.FileName);
                    //Creo una variable y le extraigo la extensión
                    String extension = System.IO.Path.GetExtension(ruta + file.FileName);
                    //Si ruta mas nombre más extensión ya existe le cambias el nombre.
                    if (modelo.CheckIfExists(ruta + nombre + extension))//Bucle para cambiar el nombre
                    {
                        //Cambio el nombre del fichero añadiendole la fecha de subida,
                        //diciendole que la introduzca con guiones no con barras y fuera con barras fallaria.
                        nombre += DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss");
                    }
                    ruta += nombre + extension;
                    //Meto el fichero en la ruta con el nombre especificado.
                    modelo.SubirArchivo(ruta, file);
                    ViewBag.Error = modelo.error;
                    ViewBag.Correcto = modelo.Confirmacion;
                    //Intrucciones para guardar el documento en la base de datos.

                    //Creo un documento "doc"
                    Documento doc = new Documento();
                    doc.Documentacion = ruta;//la ruta del documento
                    int id = int.Parse(Request.QueryString["idActuacion"]);
                    doc.Actuacion = db.Actuacions.Where(a => a.Id == id).FirstOrDefault();//La actuacion con la que se relaciona el documento //TODO
                    doc.ExpedienteId = doc.Actuacion.ExpedienteId;//El id del expediente al que pertenece la actuacion
                    doc.Descripcion = Request.Form["description"];//La descripcion del documento especificada en el formulario
                    db.Documentoes.Add(doc);//Añadimos el documento a la tabla
                    db.SaveChanges();//Esto guarda los cambios en la BBDD
                    return Redirect("./");
                }
                return View();
            }
            else//Ya que no estas autenticado, te redirijo a la pagina de login
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public FileResult DescargarArchivo(int id) 
        {
            string ruta = db.Documentoes.Where(d => d.Id == id).FirstOrDefault().Documentacion; //Saco la ruta donde esta la ruta guardado el archivo
            System.IO.FileStream fs= System.IO.File.OpenRead(ruta);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(ruta);
            //Con el código anterior se pasa el archivo en forma de octeto con el fin de poder incluir cualquier extensión de archivo.
            string name = System.IO.Path.GetFileName(ruta);//Con este código se estrae el nombre del archivo en concreto.
            return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, name);//Devuelvo un archivo a partir de los datos en formato binario o de octetos con el nombre del archivo especificado.
        }
    }
}
