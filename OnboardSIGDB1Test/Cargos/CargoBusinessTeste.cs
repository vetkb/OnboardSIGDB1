using Moq;
using OnboardSIGDB1Dominio.CargoDominio.Business;
using OnboardSIGDB1Dominio.CargoDominio.Interfaces;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnboardSIGDB1Test.Cargos
{
    public class CargoBusinessTeste
    {
        Mock<ICargoRepositorio> _mock;
        private List<Cargo> _cargos;

        public CargoBusinessTeste(Mock<ICargoRepositorio> mock)
        {
            _mock = mock;
            _cargos = CriacaoListaDeCargo();
        }

        private List<Cargo> CriacaoListaDeCargo()
        {
            return new List<Cargo>() {
                new Cargo(1, "cargo 1"),
                new Cargo(2, "cargo 2"),
                new Cargo(3, "cargo 3"),
                new Cargo(4, "cargo 4"),
            };
        }

        //[Fact]
        //public void DeveInstanciarUmCargoComDescricaoSomente()
        //{
        //    string descricao = "teste";
        //    Cargo cargo = new Cargo(descricao);

        //    Assert.Equal(descricao, cargo.Descricao);
        //}

        //[Fact]
        //public void DeveInstanciarUmCargoComTodosOsCamposPreenchidos()
        //{
        //    int id = 1;
        //    string descricao = "teste";
        //    Cargo cargo = new Cargo(id, descricao);

        //    Assert.Equal(id, cargo.Id);
        //    Assert.Equal(descricao, cargo.Descricao);
        //}

        //[Fact]
        //public void DeveRetornarListaDeCargoContendo3Cargos()
        //{
            //int cargoId = 0;
            //var cargoFaker = new Faker<Cargo>().StrictMode(true)
            //    .RuleFor(o => o.Id, f => cargoId++)
            //    .RuleFor(o => o.Descricao, f => f.Lorem.Word());

            //var cargosFaker = cargoFaker.Generate(3).ToList();

            //_mock.Setup(x => x.GetTodosCargos()).Returns(cargosFaker);

            //var cargoBusiness = new CargoBusiness(_mock.Object);

            //var cargos = cargoBusiness.GetTodosCargos();

            //Assert.True(cargos.Count == 3);
        //}

        //[Fact]
        //public void DeveRetornarListaDeCargo()
        //{

        //}
    }
}
