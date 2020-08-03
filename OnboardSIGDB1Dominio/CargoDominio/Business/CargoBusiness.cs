using OnboardSIGDB1Dominio.CargoDominio.Interfaces;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.CargoDominio.Business
{
    public class CargoBusiness : ICargoBusiness
    {
        ICargoRepositorio _iCargoRepositorio;

        public CargoBusiness(ICargoRepositorio iCargoRepositorio)
        {
            _iCargoRepositorio = iCargoRepositorio;
        }

        public List<Cargo> GetTodosCargos()
        {
            return _iCargoRepositorio.GetTodosCargos();
        }

        public List<Cargo> PesquisarCargos(string descricao)
        {
            descricao = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();

            return _iCargoRepositorio.PesquisarCargo(descricao);
        }

        public Cargo GetCargo(int id)
        {
            return _iCargoRepositorio.GetCargo(id);
        }

        private void ValidacaoCamposCargo(Cargo cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.Descricao))
            {
                throw new ArgumentNullException("Descrição obrigatório", "DescricaoObrigatorio");
            }

            cargo.Descricao = cargo.Descricao.Trim();

            if (cargo.Descricao.Length > 250)
            {
                throw new ArgumentOutOfRangeException("Tamanho máximo de caracteres: 250", "TamanhoMaximo250");
            }
        }

        public void Post(Cargo cargo)
        {
            try
            {
                ValidacaoCamposCargo(cargo);

                cargo.Descricao = cargo.Descricao.Trim();

                if (PesquisarCargos(cargo.Descricao).Count > 0)
                {
                    throw new ArgumentException("Descrição utilizada", "DescricaoUtilizada");
                }

                _iCargoRepositorio.Post(cargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Put(Cargo cargo)
        {
            try
            {
                ValidacaoCamposCargo(cargo);

                if (GetCargo(cargo.Id) == null)
                {
                    throw new ArgumentNullException("Cargo não localizado", "IdInvalido");
                }

                cargo.Descricao = cargo.Descricao.Trim();

                List<Cargo> cargos = _iCargoRepositorio.PesquisarCargo(cargo.Descricao);

                if (cargos.Count > 0 && cargos.Any(x => x.Id != cargo.Id))
                {
                    throw new ArgumentException("Descrição utilizada", "DescricaoUtilizada");
                }

                _iCargoRepositorio.Put(cargo);
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
                Cargo cargo = GetCargo(id);

                if (cargo == null)
                {
                    throw new ArgumentNullException("Cargo não localizado", "IdInvalido");
                }

                _iCargoRepositorio.Delete(cargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
