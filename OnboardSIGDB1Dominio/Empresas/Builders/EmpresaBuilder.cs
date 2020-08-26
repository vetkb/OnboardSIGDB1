using OnboardSIGDB1Dominio.Empresas.Entidades;
using System;

namespace OnboardSIGDB1Dominio.Empresas.Builders
{
    public class EmpresaBuilder
    {
        private int Id = 0;
        private string Nome = string.Empty;
        private string Cnpj = string.Empty;
        private DateTime? DataFundacao = null;

        public static EmpresaBuilder Novo()
        {
            return new EmpresaBuilder();
        }

        public EmpresaBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public EmpresaBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public EmpresaBuilder ComCnpj(string cnpj)
        {
            Cnpj = cnpj;
            return this;
        }

        public EmpresaBuilder ComDataFundacao(DateTime? dataFundacao)
        {
            DataFundacao = dataFundacao;
            return this;
        }

        public Empresa Build()
        {
            return new Empresa(Id, Nome, Cnpj, DataFundacao);
        }
    }
}
