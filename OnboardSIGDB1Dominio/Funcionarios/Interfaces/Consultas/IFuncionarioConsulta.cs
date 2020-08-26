using OnboardSIGDB1Dominio.Funcionarios.Dtos;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Funcionarios.Interfaces.Consultas
{
    public interface IFuncionarioConsulta
    {
        List<Funcionario> ObterTodos();

        List<Funcionario> ObterPorFiltro(FiltroFuncionarioDto filtro);

        Funcionario ObterPorId(int id);
    }
}
