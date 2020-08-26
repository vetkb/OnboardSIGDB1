using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio.Cargos.Consultas
{
    public class CargoConsulta : ICargoConsulta
    {
        ICargoRepositorio _cargoRepositorio;

        public CargoConsulta(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public Cargo ObterPorId(int id)
        {
            return _cargoRepositorio.ObterPorId(id);
        }

        public List<Cargo> ObterTodos()
        {
            return _cargoRepositorio.ObterTodos();
        }

        public List<Cargo> Pesquisar(FiltroCargoDto filtro)
        {
            return _cargoRepositorio.ObterPorDescricao(filtro.Descricao);
        }
    }
}
