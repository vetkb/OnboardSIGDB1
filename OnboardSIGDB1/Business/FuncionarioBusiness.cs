using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.DAL;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Business
{
    public class FuncionarioBusiness
    {
        OnboardContext _context;

        public FuncionarioBusiness(OnboardContext context)
        {
            _context = context;
        }

        public List<Funcionario> GetTodosFuncionarios()
        {
            return _context.Funcionarios.Include(x => x.Empresa).Include(x => x.Cargo).ToList();
        }

        public List<Funcionario> GetFuncionarios(string nome, string cpf, DateTime? dataContratacao)
        {
            nome = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim();
            cpf = string.IsNullOrWhiteSpace(cpf) ? null : HelperCpf.RemoveMascara(cpf);

            List<Funcionario> funcionarios = _context.Funcionarios.Where(x =>
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
                .Include(x => x.Cargo)
                .Include(x => x.Empresa)
                .FirstOrDefault(x => x.Id == id);
        }

        private void ValidacaoCamposFuncionario(Funcionario funcionario)
        {
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
            {
                throw new ArgumentNullException("Nome obrigatório", "NomeObrigatorio");
            }

            if (funcionario.Nome.Length > 150)
            {
                throw new ArgumentOutOfRangeException("Tamanho máximo de caracteres: 150", "TamanhoMaximo150");
            }

            if (string.IsNullOrWhiteSpace(funcionario.Cpf))
            {
                throw new ArgumentNullException("CPF obrigatório", "CpfObrigatorio");
            }

            if (!HelperCpf.Valido(funcionario.Cpf))
            {
                throw new ArgumentException("CPF inválido", "CpfInvalido");
            }
        }

        public void Post(Funcionario funcionario)
        {
            try
            {
                ValidacaoCamposFuncionario(funcionario);

                funcionario.Cpf = HelperCpf.RemoveMascara(funcionario.Cpf);

                if (_context.Funcionarios.Any(x => x.Cpf.Equals(funcionario.Cpf)))
                {
                    throw new ArgumentException("CPF utilizado", "CpfUtilizado");
                }

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
                if (!_context.Funcionarios.Any(x => x.Id == funcionario.Id))
                {
                    throw new ArgumentNullException("Funcionario não localizado", "IdInvalido");
                }

                ValidacaoCamposFuncionario(funcionario);

                funcionario.Cpf = HelperCpf.RemoveMascara(funcionario.Cpf);

                if (_context.Funcionarios.Any(x => x.Cpf.Equals(funcionario.Cpf) && x.Id != funcionario.Id))
                {
                    throw new ArgumentException("CPF utilizado", "CpfUtilizado");
                }

                _context.Funcionarios.Update(funcionario);
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
                Funcionario funcionario = GetFuncionario(id);

                if (funcionario == null)
                {
                    throw new ArgumentNullException("Funcionario não localizado", "IdInvalido");
                }

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
