using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1Dominio.CargoDominio.Interfaces;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.CargoDominio.Repositorios
{
    public class CargoRepositorio : ICargoRepositorio
    {
        OnboardContext _context;

        public CargoRepositorio(OnboardContext context)
        {
            _context = context;
        }

        public List<Cargo> GetTodosCargos()
        {
            return _context.Cargos.AsNoTracking().ToList();
        }

        public List<Cargo> PesquisarCargo(string descricao)
        {
            return _context.Cargos.AsNoTracking().Where(x =>
                x.Descricao.Equals(string.IsNullOrWhiteSpace(descricao) ? x.Descricao : descricao)
            ).ToList();
        }

        public Cargo GetCargo(int id)
        {
            return _context.Cargos.AsNoTracking().FirstOrDefault(x => x.Id == id);
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
