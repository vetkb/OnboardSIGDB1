﻿using System;

namespace OnboardSIGDB1.DTO.Funcionario
{
    public class CadastroFuncionarioDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataContratacao { get; set; }
        public int? EmpresaId { get; set; }
        public int? CargoId { get; set; }
    }
}
