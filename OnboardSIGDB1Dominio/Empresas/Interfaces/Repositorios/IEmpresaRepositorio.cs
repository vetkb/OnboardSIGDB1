using OnboardSIGDB1Dominio.Empresas.Entidades;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios
{
    public interface IEmpresaRepositorio
    {
        List<Empresa> ObterTodas();

        List<Empresa> ObterPorFiltro(string nome, string cnpj, DateTime? dataFundacao);

        Empresa ObterPorId(int id);

        void Post(Empresa empresa);

        void Put(Empresa empresa);

        void Delete(Empresa empresa);
    }
}
