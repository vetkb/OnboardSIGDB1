using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1.DAL.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(14)]
        public string Cnpj { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DataFundacao { get; set; }

        public List<Funcionario> Funcionarios { get; set; }
    }
}
