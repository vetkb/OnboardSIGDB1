using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1Dominio.Funcionarios.Dtos
{
    public class CadastroFuncionarioDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}
