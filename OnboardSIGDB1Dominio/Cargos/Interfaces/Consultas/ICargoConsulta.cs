using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Cargos.Interfaces.Consultas
{
    public interface ICargoConsulta
    {
        List<Cargo> ObterTodos();
        List<Cargo> Pesquisar(FiltroCargoDto filtro);
        Cargo ObterPorId(int id);
    }
}
