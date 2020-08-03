using OnboardSIGDB1Dominio.EmpresaDominio.Helpers;
using OnboardSIGDB1Dominio.EmpresaDominio.Interfaces;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.EmpresaDominio.Business
{
    public class EmpresaBusiness : IEmpresaBusiness
    {
        IEmpresaRepositorio _iEmpresaRepositorio;

        public EmpresaBusiness(IEmpresaRepositorio iEmpresaRepositorio)
        {
            _iEmpresaRepositorio = iEmpresaRepositorio;
        }

        public List<Empresa> GetTodasEmpresas()
        {
            return _iEmpresaRepositorio.GetTodasEmpresas();
        }

        public List<Empresa> GetEmpresas(string nome, string cnpj, DateTime? dataFundacao)
        {
            nome = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim();
            cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : HelperCnpj.RemoveMascara(cnpj);

            return _iEmpresaRepositorio.GetEmpresas(nome, cnpj, dataFundacao);
        }

        public Empresa GetEmpresa(int id)
        {
            return _iEmpresaRepositorio.GetEmpresa(id);
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

                if (_iEmpresaRepositorio.GetEmpresas(string.Empty, empresa.Cnpj, null).Count > 0)
                {
                    throw new ArgumentException("CNPJ utilizado", "CnpjUtilizado");
                }

                _iEmpresaRepositorio.Post(empresa);
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
                if (_iEmpresaRepositorio.GetEmpresa(empresa.Id) == null)
                {
                    throw new ArgumentException("Empresa não localizada", "IdInvalido");
                }

                ValidacaoCamposEmpresa(empresa);

                empresa.Cnpj = HelperCnpj.RemoveMascara(empresa.Cnpj);

                List<Empresa> empresas = _iEmpresaRepositorio.GetEmpresas(string.Empty, empresa.Cnpj, null);

                if (empresas.Count > 0 && empresas.Any(x => x.Id != empresa.Id))
                {
                    throw new ArgumentException("CNPJ utilizado", "CnpjUtilizado");
                }

                _iEmpresaRepositorio.Put(empresa);
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

                _iEmpresaRepositorio.Delete(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
