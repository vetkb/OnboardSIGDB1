using System;

namespace OnboardSIGDB1.DTO.Funcionario
{
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataContratacao { get; set; }
        public int? EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public int? CargoId { get; set; }
        public string CargoDescricao { get; set; }
    }
}
