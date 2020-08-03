using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        List<Funcionario> GetTodosFuncionarios();

        List<Funcionario> GetFuncionarios(string nome, string cpf, DateTime? dataContratacao);

        Funcionario GetFuncionario(int id);

        void Post(Funcionario funcionario);

        void Put(Funcionario funcionario);

        void Delete(Funcionario funcionario);
    }
}
