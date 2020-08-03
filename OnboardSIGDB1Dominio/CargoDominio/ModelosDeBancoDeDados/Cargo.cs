using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados
{
    [Table("Cargos")]
    public class Cargo
    {
        public Cargo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Cargo(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Descricao { get; set; }
    }
}
