using System;

namespace OnboardSIGDB1.DTO.Empresa
{
    public class CadastroEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
