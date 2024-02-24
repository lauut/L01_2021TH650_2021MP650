using System.ComponentModel.DataAnnotations;

namespace L01_2021TH650_2021MP650.Models
{
    public class platos
    {
        [Key]
        public int platoId { get; set; }
        public string nombrePlato { get; set; }
        public decimal precio {  get; set; }
        
    }
}
