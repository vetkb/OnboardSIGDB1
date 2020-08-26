using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System;

namespace OnboardSIGDB1Dominio.Funcionarios.Builders
{
    public class FuncionarioBuilder
    {
        private int Id { get; set; }
        private string Nome { get; set; }
        private string Cpf { get; set; }
        private DateTime? DataContratacao { get; set; }
        private int? EmpresaId { get; set; }
        private int? CargoId { get; set; }

        public static FuncionarioBuilder Novo()
        {
            return new FuncionarioBuilder();
        }

        public FuncionarioBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public FuncionarioBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public FuncionarioBuilder ComCpf(string cpf)
        {
            Cpf = cpf;
            return this;
        }

        public FuncionarioBuilder ComDataContratacao(DateTime? dataContratacao)
        {
            DataContratacao = dataContratacao;
            return this;
        }

        public FuncionarioBuilder ComEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
            return this;
        }

        public FuncionarioBuilder ComCargoId(int cargoId)
        {
            CargoId = cargoId;
            return this;
        }

        public Funcionario Build()
        {
            return new Funcionario(Id, Nome, Cpf, DataContratacao, EmpresaId, CargoId);
        }
    }
}
