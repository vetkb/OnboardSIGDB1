using System;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.DTO
{
    public class FiltroFuncionarioDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataContratacao { get; set; }
    }
}
