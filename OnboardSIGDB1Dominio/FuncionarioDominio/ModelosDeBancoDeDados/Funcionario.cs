using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(11)]
        public string Cpf { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DataContratacao { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int? CargoId { get; set; }
        public Cargo Cargo { get; set; }
    }
}
