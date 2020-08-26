using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Helpers;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Empresas.Consultas
{
    public class EmpresaConsulta : IEmpresaConsulta
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaConsulta(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public Empresa ObterPorId(int id)
        {
            return _empresaRepositorio.ObterPorId(id);
        }

        public List<Empresa> ObterPorFiltro(FiltroEmpresaDto filtro)
        {
            filtro.Nome = string.IsNullOrWhiteSpace(filtro.Nome) ? null : filtro.Nome.Trim();
            filtro.Cnpj = string.IsNullOrWhiteSpace(filtro.Cnpj) ? null : CnpjHelper.RemoveMascara(filtro.Cnpj);

            return _empresaRepositorio.ObterPorFiltro(filtro.Nome, filtro.Cnpj, filtro.DataFundacao);
        }

        public List<Empresa> ObterTodas()
        {
            return _empresaRepositorio.ObterTodas();
        }
    }
}
