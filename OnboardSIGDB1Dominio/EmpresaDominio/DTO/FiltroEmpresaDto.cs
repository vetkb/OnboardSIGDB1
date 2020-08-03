using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1Dominio.EmpresaDominio.DTO
{
    public class FiltroEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
