using OnboardSIGDB1Dominio.Funcionarios.Dtos;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Funcionarios.Consultas
{
    public class FuncionarioConsulta : IFuncionarioConsulta
    {
        IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioConsulta(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public List<Funcionario> ObterPorFiltro(FiltroFuncionarioDto filtro)
        {
            return _funcionarioRepositorio.ObterPorFiltro(filtro.Nome, filtro.Cpf, filtro.DataContratacao);
        }

        public Funcionario ObterPorId(int id)
        {
            return _funcionarioRepositorio.ObterPorId(id);
        }

        public List<Funcionario> ObterTodos()
        {
            return _funcionarioRepositorio.ObterTodos();
        }
    }
}
