using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.DAL;
using OnboardSIGDB1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Business
{
    public class CargoBusiness
    {
        OnboardContext _context;

        public CargoBusiness(OnboardContext context)
        {
            _context = context;
        }

        public List<Cargo> GetTodosCargos()
        {
            return _context.Cargos.ToList();
        }

        public List<Cargo> GetCargos(string descricao)
        {
            descricao = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();

            return _context.Cargos.Where(x =>
                x.Descricao.Equals(string.IsNullOrWhiteSpace(descricao) ? x.Descricao : descricao)
            ).ToList();
        }

        public Cargo GetCargo(int id)
        {
            return _context.Cargos.Find(id);
        }

        private void ValidacaoCamposCargo(Cargo cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.Descricao))
            {
                throw new ArgumentNullException("DescricaoObrigatorio");
            }

            cargo.Descricao = cargo.Descricao.Trim();

            if (cargo.Descricao.Length > 250)
            {
                throw new ArgumentOutOfRangeException("TamanhoMaximo250");
            }
        }

        public void Post(Cargo cargo)
        {
            try
            {
                ValidacaoCamposCargo(cargo);

                cargo.Descricao = cargo.Descricao.Trim();

                if (_context.Cargos.Any(x => x.Descricao.Equals(cargo.Descricao)))
                {
                    throw new Exception("DescricaoUtilizada");
                }

                _context.Cargos.Add(cargo);
                _context.SaveChanges();
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
                if (!_context.Cargos.Any(x => x.Id == cargo.Id))
                {
                    throw new ArgumentNullException("IdInvalido");
                }

                ValidacaoCamposCargo(cargo);

                cargo.Descricao = cargo.Descricao.Trim();

                if (_context.Cargos.Any(x => x.Descricao.Equals(cargo.Descricao) && x.Id != cargo.Id))
                {
                    throw new Exception("DescricaoUtilizada");
                }

                _context.Cargos.Update(cargo);
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
                Cargo cargo = GetCargo(id);

                if (cargo == null)
                {
                    throw new ArgumentNullException("IdInvalido");
                }

                _context.Cargos.Remove(cargo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
