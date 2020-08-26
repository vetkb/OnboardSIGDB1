using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1Dominio.Empresas.Dtos
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
