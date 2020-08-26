using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infra.Data.Funcionarios.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        OnboardContext _context;

        public FuncionarioRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Funcionario> ObterTodos()
        {
            return _context.Funcionarios
                .Include(x => x.Empresa)
                .Include(x => x.Cargo)
                .ToList();
        }

        public List<Funcionario> ObterPorFiltro(string nome, string cpf, DateTime? dataContratacao)
        {
            List<Funcionario> funcionarios = _context.Funcionarios
                .Include(x => x.Cargo)
                .Include(x => x.Empresa)
                .Where(x =>
                    x.Nome.Trim().ToLower().Equals(string.IsNullOrWhiteSpace(nome) ? x.Nome.Trim().ToLower() : nome.Trim().ToLower()) &&
                    x.Cpf.Equals(string.IsNullOrWhiteSpace(cpf) ? x.Cpf : cpf)
            ).Include(x => x.Empresa).Include(x => x.Cargo).ToList();

            if (dataContratacao.HasValue)
            {
                funcionarios = funcionarios.Where(x => x.DataContratacao.HasValue && x.DataContratacao.Value.Date.Equals(dataContratacao.Value.Date)).ToList();
            }

            return funcionarios;
        }

        public Funcionario ObterPorId(int id)
        {
            return _context.Funcionarios
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
