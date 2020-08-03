using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.CargoDominio.Interfaces
{
    public interface ICargoRepositorio
    {
        List<Cargo> GetTodosCargos();

        List<Cargo> PesquisarCargo(string descricao);

        Cargo GetCargo(int id);

        void Post(Cargo cargo);

        void Put(Cargo cargo);

        void Delete(Cargo cargo);
    }
}
