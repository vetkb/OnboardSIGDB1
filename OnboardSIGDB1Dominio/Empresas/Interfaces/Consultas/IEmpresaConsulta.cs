using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Empresas.Interfaces.Consultas
{
    public interface IEmpresaConsulta
    {
        List<Empresa> ObterTodas();

        List<Empresa> ObterPorFiltro(FiltroEmpresaDto filtro);

        Empresa ObterPorId(int id);
    }
}
