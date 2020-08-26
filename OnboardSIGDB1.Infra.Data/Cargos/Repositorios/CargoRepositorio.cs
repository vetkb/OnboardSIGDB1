using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infra.Data.Cargos.Repositorios
{
    public class CargoRepositorio : ICargoRepositorio
    {
        OnboardContext _context;

        public CargoRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Cargo> ObterTodos()
        {
            return _context.Cargos.ToList();
        }

        public List<Cargo> ObterPorDescricao(string descricao)
        {
                return _context.Cargos.Where(x => x.Descricao.Trim().ToLower() == descricao.Trim().ToLower()).ToList();
        }

        public Cargo ObterPorId(int id)
        {
            return _context.Cargos.Include(x => x.Funcionarios).FirstOrDefault(x => x.Id == id);
        }

        public void Post(Cargo cargo)
        {
            try
            {
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
                _context.Cargos.Update(cargo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Cargo cargo)
        {
            try
            {
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
