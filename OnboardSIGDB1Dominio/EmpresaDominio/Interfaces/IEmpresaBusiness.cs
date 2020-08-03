using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.EmpresaDominio.Interfaces
{
    public interface IEmpresaBusiness
    {
        List<Empresa> GetTodasEmpresas();

        List<Empresa> GetEmpresas(string nome, string cnpj, DateTime? dataFundacao);

        Empresa GetEmpresa(int id);

        void Post(Empresa empresa);

        void Put(Empresa empresa);

        void Delete(int id);
    }
}
