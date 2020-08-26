using OnboardSIGDB1Dominio.Cargos.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios
{
    public interface ICargoRepositorio
    {
        List<Cargo> ObterTodos();

        List<Cargo> ObterPorDescricao(string descricao);

        Cargo ObterPorId(int id);

        void Post(Cargo cargo);

        void Put(Cargo cargo);

        void Delete(Cargo cargo);
    }
}
