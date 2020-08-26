using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infra.Data.Empresas.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        OnboardContext _context;

        public EmpresaRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Empresa> ObterTodas()
        {
            return _context.Empresas.ToList();
        }

        public List<Empresa> ObterPorFiltro(string nome, string cnpj, DateTime? dataFundacao)
        {
            List<Empresa> empresas = _context.Empresas.Where(x =>
                x.Nome.Trim().ToLower().Equals(string.IsNullOrWhiteSpace(nome) ? x.Nome.Trim().ToLower() : nome.Trim().ToLower()) &&
                x.Cnpj.Equals(string.IsNullOrWhiteSpace(cnpj) ? x.Cnpj : cnpj)
            ).ToList();

            if (dataFundacao.HasValue)
            {
                empresas = empresas.Where(x => x.DataFundacao.HasValue && x.DataFundacao.Value.Date.Equals(dataFundacao.Value.Date)).ToList();
            }

            return empresas;
        }

        public Empresa ObterPorId(int id)
        {
            return _context.Empresas
                .Include(x => x.Funcionarios)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Post(Empresa empresa)
        {
            try
            {
                _context.Empresas.Add(empresa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Put(Empresa empresa)
        {
            try
            {
                _context.Empresas.Update(empresa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Empresa empresa)
        {
            try
            {
                _context.Empresas.Remove(empresa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
