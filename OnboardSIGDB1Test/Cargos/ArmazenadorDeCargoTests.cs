using Bogus;
using Moq;
using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Builders;
using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Cargos.Servicos;
using OnboardSIGDB1Test.Builders;
using System.Collections.Generic;
using Xunit;

namespace OnboardSIGDB1Test.Cargos
{
    public class ArmazenadorDeCargoTests
    {
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly Mock<INotificationContext> _notificationContextMock;
        private readonly IArmazenadorDeCargo _armazenadorDeCargo;
        
        private readonly string _descricaoValida;
        private readonly string _descricaoInvalida;
        private readonly Faker _faker;

        public ArmazenadorDeCargoTests()
        {
            _faker = FakerBuilder.Novo().Build();
            _descricaoValida = _faker.Random.String2(250);
            _descricaoInvalida = _faker.Random.String2(251);

            _notificationContextMock = new Mock<INotificationContext>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();
            _armazenadorDeCargo = new ArmazenadorDeCargo(_cargoRepositorioMock.Object, _notificationContextMock.Object);            
        }

        private CargoDto GerarCargoDtoParaAdicionarCargo(string descricao)
        {
            return new CargoDto() {
                Id = 0,
                Descricao = descricao
            };
        }

        private CargoDto GerarCargoDtoParaAtualizarCargo(string descricao)
        {
            return new CargoDto()
            {
                Id = 1,
                Descricao = descricao
            };
        }

        [Fact]
        public void DeveAdicionarCargo()
        {
            CargoDto dto = GerarCargoDtoParaAdicionarCargo(_descricaoValida);

            _cargoRepositorioMock.Setup(x => x.ObterPorDescricao(dto.Descricao)).Returns(new List<Cargo>());

            _armazenadorDeCargo.Armazenar(dto);

            _cargoRepositorioMock.Verify(x => x.Post(
                It.Is<Cargo>(
                    c => c.Descricao == dto.Descricao
                )), 
                Times.Once
            );
        }

        [Fact]
        public void DeveNotificarQuandoAdicionarDescricaoForMuitoGrande()
        {
            CargoDto dto = GerarCargoDtoParaAdicionarCargo(_descricaoInvalida);

            _armazenadorDeCargo.Armazenar(dto);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo inválido"),
                Times.Once
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveNotificarQuandoAdicionarDescricaoForInvalida(string descricaoInvalida)
        {
            CargoDto dto = GerarCargoDtoParaAdicionarCargo(descricaoInvalida);

            _armazenadorDeCargo.Armazenar(dto);

            _notificationContextMock.Verify(
                x => 
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo inválido"), 
                Times.Once
            );
        }

        [Fact]
        public void DeveNotificarQuandoAdicionarDescricaoJaFoiUtilizada()
        {
            CargoDto dto = GerarCargoDtoParaAdicionarCargo(_descricaoValida);

            var cargoJaExiste = new List<Cargo> {
                CargoBuilder.Novo()
                .ComDescricao(_descricaoValida)
                .Build()
            };

            _cargoRepositorioMock.Setup(x => x.ObterPorDescricao(_descricaoValida)).Returns(cargoJaExiste);

            _armazenadorDeCargo.Armazenar(dto);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Descrição utilizada"),
                Times.Once
            );
        }

        [Fact]
        public void DeveAtualizarCargo()
        {
            CargoDto dto = GerarCargoDtoParaAtualizarCargo(_descricaoValida);
            Cargo cargo = CargoBuilder.Novo()
                .ComId(dto.Id)
                .ComDescricao(dto.Descricao)
                .Build();

            _cargoRepositorioMock.Setup(x => x.ObterPorId(dto.Id)).Returns(cargo);
            _cargoRepositorioMock.Setup(x => x.ObterPorDescricao(dto.Descricao)).Returns(new List<Cargo>());

            _armazenadorDeCargo.Armazenar(dto);

            _cargoRepositorioMock.Verify(x => x.Put(
                It.Is<Cargo>(
                    c => c.Descricao == dto.Descricao
                )),
                Times.Once
            );
        }

        [Fact]
        public void DeveNotificarQuandoAtualizarUmCargoQueNaoFoiEncontrado()
        {
            CargoDto dto = GerarCargoDtoParaAtualizarCargo(_descricaoValida);

            _armazenadorDeCargo.Armazenar(dto);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo não encontrado"),
                Times.Once
            );
        }

        [Fact]
        public void DeveNotificarQuandoAtualizarCargoComDescricaoQueJaFoiUtilizada()
        {
            CargoDto dto = GerarCargoDtoParaAtualizarCargo(_descricaoValida);

            Cargo cargo = CargoBuilder.Novo()
                .ComId(dto.Id)
                .ComDescricao(dto.Descricao)
                .Build();

            var cargoJaExiste = new List<Cargo> {
                CargoBuilder.Novo().ComId(dto.Id).ComDescricao(dto.Descricao).Build(),
                CargoBuilder.Novo().ComId(2).ComDescricao(_descricaoValida).Build()
            };

            _cargoRepositorioMock.Setup(x => x.ObterPorId(dto.Id)).Returns(cargo);
            _cargoRepositorioMock.Setup(x => x.ObterPorDescricao(_descricaoValida)).Returns(cargoJaExiste);

            _armazenadorDeCargo.Armazenar(dto);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Descrição utilizada"),
                Times.Once
            );
        }
    }
}
