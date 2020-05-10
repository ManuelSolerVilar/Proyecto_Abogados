using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class Documento
    {
        public int Id { get; set; }
        [Display(Name = "Expediente")]
        public int ExpedienteId { get; set; }
        [Display(Name = "Descripción")] public string Descripcion { get; set; }
        [Display(Name = "Documento")] public string Documentacion { get; set; }
        public virtual Actuacion Actuacion { get; set; }
    }
}