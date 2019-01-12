using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Account
    {
    }

    public class CurrencyDenominationViewModel
    {
        public Currency Currency { get; set; }

        public List<Currency_Denomination> Denominations { get; set; }
    }

    public class OfficeUserViewModel
    {
        public Office Office { get; set; }

        public List<UserViewModel> UserViewModels { get; set; }
    }

    public class ModuleViewModel
    {
        public Module Module { get; set; }

        public List<SubModule> SubModules { get; set; }
    }

    public class UserViewModel
    {
        public int USER_ID { get; set; }

        [Display(Name = "Cliente")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un Cliente")]
        public int CUSTOMER_ID { get; set; }

        [Display(Name = "Rol")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un Rol")]
        public int ROLE_ID { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(40,
            ErrorMessage = "El campo {0} no debe superar {1} caracteres y como mínimo debe tener {2} caracteres",
            MinimumLength = 3)]
        public string IDENTIFICATION { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(40,
            ErrorMessage = "El campo {0} no debe superar {1} caracteres y como mínimo debe tener {2} caracteres",
            MinimumLength = 3)]
        public string NAME { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(40,
                ErrorMessage = "El campo {0} no debe superar {1} caracteres y como mínimo debe tener {2} caracteres",
                MinimumLength = 3)]
        public string USERNAME { get; set; }

        [Display(Name = "Cotraseña")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener una longitud máxima de {1} carácter")]
        [Display(Name = "E-Mail")]        
        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }

        [Display(Name = "Imagen")]
        public byte[] IMAGE { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Debe ingresar un télefono válido")]
        [StringLength(10,
            ErrorMessage = "El campo {0} no debe superar {1} caracteres y como mínimo debe tener {2} caracteres",
            MinimumLength = 7)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string PHONE { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string OFFICE_NAME { get; set; }

        public string ROL_NAME { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagePathFile { get; set; }
    }

    public class TransactPaypadViewModel
    {
        [Display(Name ="Pay+")]
        public string NAME { get; set; }

        [Display(Name = "Trámites")]
        public string DESCRIPTION { get; set; }

    }

    public class LoginViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]        
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }

    }

    public class DevicesForPayPadViewModel
    {
        public PayPad PayPad { get; set; }

        public List<Device> Devices { get; set; }
    }
}