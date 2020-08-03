using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.FuncionarioDominio.Interfaces;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        OnboardContext _context;

        public FuncionarioRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Funcionario> GetTodosFuncionarios()
        {
            return _context.Funcionarios.AsNoTracking().Include(x => x.Empresa).Include(x => x.Cargo).ToList();
        }

        public List<Funcionario> GetFuncionarios(string nome, string cpf, DateTime? dataContratacao)
        {
            List<Funcionario> funcionarios = _context.Funcionarios.AsNoTracking().Where(x =>
                x.Nome.Equals(string.IsNullOrWhiteSpace(nome) ? x.Nome : nome) &&
                x.Cpf.Equals(string.IsNullOrWhiteSpace(cpf) ? x.Cpf : cpf)
            ).Include(x => x.Empresa).Include(x => x.Cargo).ToList();

            if (dataContratacao.HasValue)
            {
                funcionarios = funcionarios.Where(x => x.DataContratacao.HasValue && x.DataContratacao.Value.Date.Equals(dataContratacao.Value.Date)).ToList();
            }

            return funcionarios;
        }

        public Funcionario GetFuncionario(int id)
        {
            return _context.Funcionarios
                .AsNoTracking()
                .Include(x => x.Cargo)
                .Include(x => x.Empresa)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Post(Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Put(Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Update(funcionario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
