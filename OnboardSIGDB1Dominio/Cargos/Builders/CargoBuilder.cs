using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Cargos.Builders
{
    public class CargoBuilder
    {
        private int Id;
        private string Descricao;
        private List<Funcionario> Funcionarios;

        public static CargoBuilder Novo()
        {
            return new CargoBuilder();
        }

        public CargoBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public CargoBuilder ComDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }

        public CargoBuilder ComFuncionarios(List<Funcionario> funcionarios)
        {
            Funcionarios = funcionarios;
            return this;
        }

        public Cargo Build()
        {
            return new Cargo(Id, Descricao, Funcionarios);
        }
    }
}
