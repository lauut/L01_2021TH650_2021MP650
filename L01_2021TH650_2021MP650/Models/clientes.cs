using System.ComponentModel.DataAnnotations;

namespace L01_2021TH650_2021MP650.Models
{
    public class clientes
    {
        [Key]
        public int clienteId { get; set; }
        public string nombreCliente {  get; set; }
        public string direccion {  get; set; }






    }
}
