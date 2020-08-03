using FluentValidation;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados
{
    [Table("Funcionarios")]
    public class Funcionario : AbstractValidator<Funcionario>
    {
        public Funcionario(
            int id,
            string nome,
            string cpf,
            DateTime? dataContratacao = null,
            int? empresaId = null,
            int? cargoId = null)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
            EmpresaId = empresaId;
            CargoId = cargoId;
        }

        public Funcionario(
            string nome, 
            string cpf, 
            DateTime? dataContratacao = null, 
            int? empresaId = null,
            int? cargoId = null)
        {
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
            EmpresaId = empresaId;
            CargoId = cargoId;
        }

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

        public bool Validar()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .Length(11);

            RuleFor(x => x.DataContratacao)
                .LessThanOrEqualTo(DateTime.Today.Date)
                .When(x => x.DataContratacao.HasValue);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
