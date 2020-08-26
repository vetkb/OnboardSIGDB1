using FluentValidation;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Empresas.Entidades
{
    public class Empresa : AbstractValidator<Empresa>
    {
        public Empresa(int id, string nome, string cnpj, DateTime? dataFundacao)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public DateTime? DataFundacao { get; set; }

        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

        public bool Validar()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .Length(14);

            RuleFor(x => x.DataFundacao.Value.Date)
                .LessThanOrEqualTo(DateTime.Today.Date)
                .When(x => x.DataFundacao.HasValue);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
