using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.DAL;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Business
{
    public class EmpresaBusiness
    {
        OnboardContext _context;

        public EmpresaBusiness(OnboardContext context)
        {
            _context = context;
        }

        public List<Empresa> GetTodasEmpresas()
        {
            return _context.Empresas.ToList();
        }

        public List<Empresa> GetEmpresas(string nome, string cnpj, DateTime? dataFundacao)
        {
            nome = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim();
            cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : HelperCnpj.RemoveMascara(cnpj);

            List<Empresa> empresas = _context.Empresas.Where(x =>
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
                .Include(x => x.Funcionarios)
                .FirstOrDefault(x => x.Id == id);
        }

        private void ValidacaoCamposEmpresa(Empresa empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nome))
            {
                throw new ArgumentNullException("Nome obrigatório", "NomeObrigatorio");
            }

            if (empresa.Nome.Length > 150)
            {
                throw new ArgumentOutOfRangeException("Tamanho máximo de caracteres: 150", "TamanhoMaximo150");
            }

            if (string.IsNullOrWhiteSpace(empresa.Cnpj))
            {
                throw new ArgumentNullException("CNPJ obrigatório", "CnpjObrigatorio");
            }

            if (!HelperCnpj.Valido(empresa.Cnpj))
            {
                throw new ArgumentException("CNPJ inválido", "CnpjInvalido");
            }
        }

        public void Post(Empresa empresa)
        {
            try
            {
                ValidacaoCamposEmpresa(empresa);

                empresa.Cnpj = HelperCnpj.RemoveMascara(empresa.Cnpj);

                if (_context.Empresas.Any(x => x.Cnpj.Equals(empresa.Cnpj)))
                {
                    throw new ArgumentException("CNPJ utilizado", "CnpjUtilizado");
                }

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
                if (!_context.Empresas.Any(x => x.Id == empresa.Id))
                {
                    throw new ArgumentException("Empresa não localizada", "IdInvalido");
                }

                ValidacaoCamposEmpresa(empresa);

                empresa.Cnpj = HelperCnpj.RemoveMascara(empresa.Cnpj);

                if (_context.Empresas.Any(x => x.Cnpj.Equals(empresa.Cnpj) && x.Id != empresa.Id))
                {
                    throw new ArgumentException("CNPJ utilizado", "CnpjUtilizado");
                }

                _context.Empresas.Update(empresa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Empresa empresa = GetEmpresa(id);

                if (empresa == null)
                {
                    throw new ArgumentException("Empresa não localizada", "IdInvalido");
                }

                if (empresa.Funcionarios.Count > 0)
                {
                    throw new ArgumentException("Existem funcionários associados a essa empresa", "FuncionarioAssociadoEmpresa");
                }

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
