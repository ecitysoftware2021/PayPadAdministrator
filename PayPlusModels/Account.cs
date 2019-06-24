using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPlusModels
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
        [Display(Name = "Pay+")]
        public string NAME { get; set; }

        [Display(Name = "Trámites")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "ID")]
        public int TRANSACTION_TYPE_ID { get; set; }

    }

    public class BalancePaypadViewModel
    {
        [Display(Name = "Responsable")]
        public string NAME { get; set; }

        [Display(Name = "Fecha")]
        public DateTime DATE { get; set; }

        public string DateString { get; set; }

        public int PAYPAD_ID { get; set; }

    }

    public partial class BalancingDetailResultViewModel
    {
        public Nullable<int> AMOUNT_NEW { get; set; }
        public int AMOUNT { get; set; }
        public int ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public int VALUE { get; set; }
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public int DEVICE_TYPE_ID { get; set; }
    }

    public partial class BalancingPayplusResultViewModel
    {
        public int PAYPAD_BALANCING_ID { get; set; }
        public int USER_ID { get; set; }
        public int PAYPAD_ID { get; set; }
        public DateTime DATE_BEGIN { get; set; }
        public Nullable<System.DateTime> DATE_END { get; set; }
        public decimal TOTAL_BANACE { get; set; }
        public Nullable<decimal> TOTAL_RECOLLECTED { get; set; }
        public string DESCRIPTION { get; set; }
        public bool STATE { get; set; }
    }

    public partial class SP_GET_UPLOAD_PAYPAD_Result
    {
        public int PAYPAD_UPLOAD_ID { get; set; }
        public int PAYPAD_ID { get; set; }
        public int USER_ID { get; set; }
        public Nullable<decimal> TOTAL_UPLOAD { get; set; }
        public decimal TOTAL_IN { get; set; }
        public System.DateTime DATE_BEGIN { get; set; }
        public Nullable<System.DateTime> DATE_END { get; set; }
        public string DESCRIPTION { get; set; }
        public bool STATE { get; set; }
    }

    public partial class SP_GET_UPLOAD_DETAILS_Result
    {
        public int AMOUNT_NEW { get; set; }
        public Nullable<int> AMOUNT { get; set; }
        public int ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public int VALUE { get; set; }
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public int DEVICE_TYPE_ID { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserName { get; set; }
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

    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]

        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("NewPassword", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]

        public string ConfirmPassword { get; set; }
    }

    public class DevicesForPayPadViewModel
    {
        public PayPad PayPad { get; set; }

        public List<Device> Devices { get; set; }
    }

    public partial class SP_GET_CHARGE_DATA_Result
    {
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public byte[] IMAGE { get; set; }
        public int VALUE { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una cantidad para el cargue")]
        public int STACKER_QUANTITY { get; set; }
        public int DEVICE_PAYPAD_DETAIL_ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
    }

    public partial class SP_GET_ARCHING_DATA_Result
    {
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public byte[] IMAGE { get; set; }
        public int VALUE { get; set; }
        public int CASHBOX_QUANTITY { get; set; }
        public int STACKER_QUANTITY { get; set; }
        public int DEVICE_PAYPAD_DETAIL_ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public string NAME { get; set; }
    }

    public class UpdateData
    {
        public List<DataDenominations> DataDenominations { get; set; }

        public int pAYPAD_ID { get; set; }

        public int uSER_ID { get; set; }

        public bool ActiveArching { get; set; }
    }

    public class DataDenominations
    {
        public int QUANTITY { get; set; }

        public string CURRENCY_DENOMINATION_ID { get; set; }

        public string vALUE { get; set; }

        public string DEVICE_PAYPAD_DETAIL_ID { get; set; }

        public string DEVICE_PAYPAD_ID { get; set; }
    }

    public partial class SP_GET_INVOICE_DATA_Result
    {
        public int INVOICE_DATA_ID { get; set; }
        [DisplayName("Prefijo")]
        public string PREFIJO { get; set; }
        [DisplayName("Resolución")]
        public string RESOLUCION { get; set; }
        [DisplayName("Fecha Resolución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHA_RESOLUCION { get; set; }
        [DisplayName("Rango Desde")]
        public double RANGO_DESDE { get; set; }
        [DisplayName("Rango Hasta")]
        public double RANGO_HASTA { get; set; }
        [DisplayName("Consecutivo rango actual")]
        public double RANGO_ACTUAL { get; set; }
        [DisplayName("Consecutivo límite de uso")]
        public int MINIMO_DISPONIBLE { get; set; }
        [DisplayName("Consecutivo habilitado")]
        [ReadOnly(true)]
        public Nullable<bool> IS_AVAILABLE { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
    }
}