using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class ResponseData
    {
        public object Número_Pedido { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public int Total { get; set; }
        public string IVA { get; set; }
        public string Medio_Pago { get; set; }
        public string Estado_Pedido { get; set; }
        public string estado_Pago { get; set; }
        public string Identificación { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido_Cliente { get; set; }
        public object Email { get; set; }
        public object Teléfono { get; set; }
        public string Tipo { get; set; }
        public int TicketId { get; set; }
        public int Cod_Transacción { get; set; }
        public string Ciclo { get; set; }
        public string Banco { get; set; }
        public string Campo_1 { get; set; }
        public string Campo_2 { get; set; }
        public string Campo_3 { get; set; }
        public string Concepto { get; set; }
    }

    public class DataCamaraComercio
    {
        public int CodeError { get; set; }
        public object Message { get; set; }
        public List<ResponseData> Data { get; set; }
    }
}