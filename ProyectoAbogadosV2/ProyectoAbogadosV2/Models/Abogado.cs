using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class Abogado
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del abogado es un campo requerido.")]
        public string NombreAbogado { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los apellidos del abogado es un campo requerido.")]
        public string ApellidosAbogado { get; set; }

        [Display(Name = "Nif")]
        [Required(ErrorMessage = "El nif del abogado es un campo obligatorio.")]
        public string NifAbogado { get; set; }

        [Display(Name = "Teléfono Movil")]
        [Required(ErrorMessage = "El Teléfono movil es un campo obligatorio.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Introduzca un teléfono valido.")]
        public string MovilAbogado { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Introduzca un teléfono valido.")]
        public string FijoAbogado { get; set; }


        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El correo electrónico del cliente es un campo requerido.")]
        [EmailAddress] public string Email { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion_Cliente { get; set; }

        [Display(Name = "Población")]
        public string Poblacion_Cliente { get; set; }

        [Display(Name = "Código Postal")]
        public string Cp_Cliente { get; set; }

        public virtual ICollection<Expediente> Expedientes { get; set; }
    }
}