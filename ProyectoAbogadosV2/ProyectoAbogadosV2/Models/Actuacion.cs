using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class Actuacion
    {
        public int Id { get; set; }
        [Display(Name = "Expediente")]
        public int ExpedienteId { get; set; }

        [Display(Name = "Fecha  de Apertura")]
        [Required(ErrorMessage = "La fecha de apertura del expediente es un campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaInicio { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaCierre { get; set; }
        [Display(Name = "Descripción")] public string Descripcion { get; set; }
        [Display(Name = "Notificaciones al cliente")] public string NotificacionCliente { get; set; }
        [Display(Name = "Notificaciones al juzgado")] public string NotificacionJuzgado { get; set; }
        public virtual Expediente Expediente { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }
    }
}