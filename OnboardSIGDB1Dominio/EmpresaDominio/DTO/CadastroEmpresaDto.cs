using System;

namespace OnboardSIGDB1Dominio.EmpresaDominio.DTO
{
    public class CadastroEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
