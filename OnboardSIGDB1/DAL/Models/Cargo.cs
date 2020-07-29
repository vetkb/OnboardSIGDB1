using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1.DAL.Models
{
    [Table("Cargos")]
    public class Cargo
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Descricao { get; set; }
    }
}
