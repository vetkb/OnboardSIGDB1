using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios
{
    public interface IFuncionarioRepositorio
    {
        List<Funcionario> ObterTodos();

        List<Funcionario> ObterPorFiltro(string nome, string cpf, DateTime? dataContratacao);

        Funcionario ObterPorId(int id);

        void Post(Funcionario funcionario);

        void Put(Funcionario funcionario);

        void Delete(Funcionario funcionario);
    }
}
