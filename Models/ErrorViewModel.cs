using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
namespace ProyectoFinal_CarritoCompras.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
