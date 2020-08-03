using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.EmpresaDominio.Helpers;
using OnboardSIGDB1Dominio.EmpresaDominio.Interfaces;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.EmpresaDominio.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        OnboardContext _context;

        public EmpresaRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Empresa> GetTodasEmpresas()
        {
            return _context.Empresas.AsNoTracking().ToList();
        }

        public List<Empresa> GetEmpresas(string nome, string cnpj, DateTime? dataFundacao)
        {
            List<Empresa> empresas = _context.Empresas.AsNoTracking().Where(x =>
                x.Nome.Equals(string.IsNullOrWhiteSpace(nome) ? x.Nome : nome) &&
                x.Cnpj.Equals(string.IsNullOrWhiteSpace(cnpj) ? x.Cnpj : cnpj)
            ).ToList();

            if (dataFundacao.HasValue)
            {
                empresas = empresas.Where(x => x.DataFundacao.HasValue && x.DataFundacao.Value.Date.Equals(dataFundacao.Value.Date)).ToList();
            }

            return empresas;
        }

        public Empresa GetEmpresa(int id)
        {
            return _context.Empresas
                .AsNoTracking()
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
