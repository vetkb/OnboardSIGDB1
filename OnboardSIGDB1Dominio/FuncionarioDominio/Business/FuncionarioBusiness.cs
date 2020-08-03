using OnboardSIGDB1Dominio.FuncionarioDominio.Helpers;
using OnboardSIGDB1Dominio.FuncionarioDominio.Interfaces;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.FuncionarioDominio.Business
{
    public class FuncionarioBusiness : IFuncionarioBusiness
    {
        readonly IFuncionarioRepositorio _iFuncionarioRepositorio;

        public FuncionarioBusiness(IFuncionarioRepositorio iFuncionarioRepositorio)
        {
            _iFuncionarioRepositorio = iFuncionarioRepositorio;
        }

        public List<Funcionario> GetTodosFuncionarios()
        {
            return _iFuncionarioRepositorio.GetTodosFuncionarios();
        }

        public List<Funcionario> GetFuncionarios(string nome, string cpf, DateTime? dataContratacao)
        {
            nome = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim();
            cpf = string.IsNullOrWhiteSpace(cpf) ? null : HelperCpf.RemoveMascara(cpf);

            return _iFuncionarioRepositorio.GetFuncionarios(nome, cpf, dataContratacao);
        }

        public Funcionario GetFuncionario(int id)
        {
            return _iFuncionarioRepositorio.GetFuncionario(id);
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

                if (_iFuncionarioRepositorio.GetFuncionarios(string.Empty, funcionario.Cpf, null).Count > 0)
                {
                    throw new ArgumentException("CPF utilizado", "CpfUtilizado");
                }

                _iFuncionarioRepositorio.Post(funcionario);
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
                if (GetFuncionario(funcionario.Id) == null)
                {
                    throw new ArgumentNullException("Funcionario não localizado", "IdInvalido");
                }

                ValidacaoCamposFuncionario(funcionario);

                funcionario.Cpf = HelperCpf.RemoveMascara(funcionario.Cpf);

                List<Funcionario> funcionarios = _iFuncionarioRepositorio.GetFuncionarios(string.Empty, funcionario.Cpf, null);

                if (funcionarios.Count > 0 && funcionarios.Any(x => x.Id != funcionario.Id))
                {
                    throw new ArgumentException("CPF utilizado", "CpfUtilizado");
                }

                _iFuncionarioRepositorio.Put(funcionario);
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

                _iFuncionarioRepositorio.Delete(funcionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
