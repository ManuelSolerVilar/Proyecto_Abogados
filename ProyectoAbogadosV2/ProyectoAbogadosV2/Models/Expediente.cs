using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class Expediente
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El título del expediente es un campo requerido.")]
        public string TituloExpediente { get; set; }

        [Display(Name = "Fecha  de Apertura")]
        [Required(ErrorMessage = "La fecha de apertura del expediente es un campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaInicio { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaCierre { get; set; }
        [Display(Name = "Descripción")] public string Descripcion { get; set; }
        [Display(Name = "Abogado")] public int AbogadoId { get; set; }
        [Display(Name = "Cliente")] public int ClienteId { get; set; }
        [Display(Name = "Jurisdicción")] public int JurisdiccionId { get; set; }
        [Display(Name = "Provisión de Fondos")] public int ProvisionFondos { get; set; }
        [Display(Name = "Total Minuta")] public int TotalMinuta { get; set; }
        //[Display(Name = "Minuta Pendiente")] public int MinutaPendiente { get; set; }

        public virtual Abogado Abogado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Jurisdiccion Jurisdiccion { get; set; }

        public virtual ICollection<Actuacion> Actuaciones { get; set; }

        //public virtual ICollection<>
    }
}