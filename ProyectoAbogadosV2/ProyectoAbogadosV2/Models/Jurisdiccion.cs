using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class Jurisdiccion
    {
        public int Id { get; set; }

        [Display(Name = "Materia")]
        [Required(ErrorMessage = "El de la materia es un campo obligatorio.")]
        public string Nombre { get; set; }
        public virtual ICollection<Expediente> Expedientes { get; set; }
    }
}