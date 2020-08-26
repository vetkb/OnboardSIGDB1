using FluentValidation;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using System;

namespace OnboardSIGDB1Dominio.Funcionarios.Entidades
{
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

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

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

            RuleFor(x => x.DataContratacao.Value.Date)
                .LessThanOrEqualTo(DateTime.Today.Date)
                .When(x => x.DataContratacao.HasValue);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
