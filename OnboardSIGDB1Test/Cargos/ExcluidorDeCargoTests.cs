using Moq;
using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Builders;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Cargos.Servicos;
using OnboardSIGDB1Dominio.Funcionarios.Builders;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using System.Collections.Generic;
using Xunit;

namespace OnboardSIGDB1Test.Cargos
{
    public class ExcluidorDeCargoTests
    {
        private readonly IExcluidorDeCargo _excluidorDeCargo;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly Mock<INotificationContext> _notificationContextMock;

        public ExcluidorDeCargoTests()
        {
            _notificationContextMock = new Mock<INotificationContext>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();
            _excluidorDeCargo = new ExcluidorDeCargo(_cargoRepositorioMock.Object, _notificationContextMock.Object);
        }

        [Fact]
        public void DeveExcluirUmCargo()
        {
            int id = 1;
            string descricao = "descricao";
            Cargo cargo = CargoBuilder.Novo()
                .ComId(id)
                .ComDescricao(descricao)
                .ComFuncionarios(new List<Funcionario>())
                .Build();

            _cargoRepositorioMock.Setup(x => x.ObterPorId(id)).Returns(cargo);

            var cargoExcluidoComSucesso = _excluidorDeCargo.Excluir(id);

            Assert.True(cargoExcluidoComSucesso);
        }

        [Fact]
        public void DeveNotificarQueExistemFuncionariosUtilizandoEsseCargo()
        {
            int id = -1;
            string descricao = "descricao";
            List<Funcionario> funcionarios = new List<Funcionario>() {
                FuncionarioBuilder.Novo().ComId(1).Build()
            };
            Cargo cargo = CargoBuilder.Novo()
                .ComId(id)
                .ComDescricao(descricao)
                .ComFuncionarios(funcionarios)
                .Build();

            _cargoRepositorioMock.Setup(x => x.ObterPorId(id)).Returns(cargo);

            var cargoExcluidoComSucesso = _excluidorDeCargo.Excluir(id);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Existem funcionários associados ao cargo"),
            Times.Once
            );
            Assert.False(cargoExcluidoComSucesso);
        }

        [Fact]
        public void DeveNotificarQueCargoNaoFoiEncontrado()
        {
            int id = -1;

            var cargoExcluidoComSucesso = _excluidorDeCargo.Excluir(id);

            _notificationContextMock.Verify(
                x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo não encontrado"),
                Times.Once
            );
            Assert.False(cargoExcluidoComSucesso);
        }
    }
}
