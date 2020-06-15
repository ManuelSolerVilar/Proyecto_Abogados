using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProyectoAbogadosV2.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es un campo obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los apellidos es un campo obligatorio.")]
        public string Apellidos { get; set; }


        [Display(Name = "Nif")]
        [Required(ErrorMessage = "El nif es un campo obligatorio.")]
        [RegularExpression("^(([A-Z]\\d{8})|(\\d{8}[A-Z]))$", ErrorMessage = "NIF incorrecto")]
        public string NifCliente { get; set; }

        [Display(Name = "Teléfono Movil")]
        [Required(ErrorMessage = "El Teléfono movil es un campo obligatorio.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Introduzca un teléfono valido.")]
        public string MovilCliente { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Introduzca un teléfono valido.")]
        public string FijoCliente { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaIngreso { get; set; }

        [Display(Name = "Fecha de baja")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaBaja { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El correo electrónico del cliente es un campo requerido.")]
        [EmailAddress] public string Email { get; set; }

        [Display(Name = "Datos de Interés")]
        public string Descripcion_Cliente { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion_Cliente { get; set; }
        [Display(Name = "Población")]
        public string Poblacion_Cliente { get; set; }

        [Display(Name = "C.P.")]
        [RegularExpression("^(?:0?[1-9]|[1-4]\\d|5[0-2])\\d{3}$", ErrorMessage = "NIF incorrecto")]
        public string Cp_Cliente { get; set; }

        public virtual ICollection<Expediente> Expedientes { get; set; }
    }
}