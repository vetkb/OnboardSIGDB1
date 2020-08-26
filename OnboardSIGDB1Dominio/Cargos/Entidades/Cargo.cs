using FluentValidation;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Cargos.Entidades
{
    public class Cargo : AbstractValidator<Cargo>
    {
        public Cargo(int id, string descricao, List<Funcionario> funcionarios)
        {
            Id = id;
            Descricao = descricao;
            Funcionarios = funcionarios;
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public List<Funcionario> Funcionarios { get; set; }

        public bool Validar()
        {
            RuleFor(x => x.Descricao).NotEmpty();
            RuleFor(x => x.Descricao).MaximumLength(250);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
