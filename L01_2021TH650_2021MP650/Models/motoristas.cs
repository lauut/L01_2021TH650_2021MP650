using System.ComponentModel.DataAnnotations;
namespace L01_2021TH650_2021MP650.Models
{
    public class motoristas
    {
        [Key]
        public int motoristaId { get; set; }
        public string nombreMotorista {  get; set; }

    }
}
