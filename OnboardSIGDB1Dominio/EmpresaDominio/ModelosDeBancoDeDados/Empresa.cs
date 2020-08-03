using FluentValidation;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados
{
    [Table("Empresas")]
    public class Empresa : AbstractValidator<Empresa>
    {
        public Empresa(int id, string nome, string cnpj, DateTime? dataFundacao)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
        }

        public Empresa(string nome, string cnpj, DateTime? dataFundacao)
        {
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
        }

        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(14)]
        public string Cnpj { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DataFundacao { get; set; }

        public List<Funcionario> Funcionarios { get; set; }

        public bool Validar()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .Length(14);

            RuleFor(x => x.DataFundacao)
                .LessThanOrEqualTo(DateTime.Today.Date)
                .When(x => x.DataFundacao.HasValue);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
