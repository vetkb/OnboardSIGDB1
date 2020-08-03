using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados
{
    [Table("Cargos")]
    public class Cargo : AbstractValidator<Cargo>
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

        public Cargo()
        {

        }

        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Descricao { get; set; }

        public bool Validar()
        {
            RuleFor(x => x.Descricao).NotEmpty();
            RuleFor(x => x.Descricao).MaximumLength(250);

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }
    }
}
