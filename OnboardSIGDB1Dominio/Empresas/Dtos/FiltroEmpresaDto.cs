using System;

namespace OnboardSIGDB1Dominio.Empresas.Dtos
{
    public class FiltroEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
