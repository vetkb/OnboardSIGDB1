using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Helpers;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Empresas.Servicos;
using OnboardSIGDB1Test.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace OnboardSIGDB1Test.Empresas
{
    public class ArmazenadorDeEmpresaTests
    {
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly Mock<INotificationContext> _notificationContextMock;
        private readonly IArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private readonly Faker _faker;

        public ArmazenadorDeEmpresaTests()
        {
            _faker = FakerBuilder.Novo().Build();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
            _notificationContextMock = new Mock<INotificationContext>();
            _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(_empresaRepositorioMock.Object, _notificationContextMock.Object);
        }

        [Fact]
        public void DeveAdicionarEmpresaSemDataFundacao()
        {
            EmpresaDto dto = new EmpresaDto()
            {
                Id = 0,
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = null
            };

            string cnpjSemMascara = CnpjHelper.RemoveMascara(dto.Cnpj);

            _empresaRepositorioMock.Setup(x => x.ObterPorFiltro(string.Empty, cnpjSemMascara, null)).Returns(new List<Empresa>());

            _armazenadorDeEmpresa.Armazenar(dto);

            _empresaRepositorioMock.Verify(x => x.Post(
                It.Is<Empresa>(
                    c => c.Nome == dto.Nome &&
                    c.Cnpj == dto.Cnpj &&
                    c.DataFundacao == dto.DataFundacao
                )),
                Times.Once);
        }

        [Fact]
        public void DeveAdicionarEmpresaComDataFundacao()
        {
            EmpresaDto dto = new EmpresaDto()
            {
                Id = 0,
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = DateTime.Now
            };

            string cnpjSemMascara = CnpjHelper.RemoveMascara(dto.Cnpj);

            _empresaRepositorioMock.Setup(x => x.ObterPorFiltro(string.Empty, cnpjSemMascara, null)).Returns(new List<Empresa>());

            _armazenadorDeEmpresa.Armazenar(dto);

            _empresaRepositorioMock.Verify(x => x.Post(
                It.Is<Empresa>(
                    c => c.Nome == dto.Nome &&
                    c.Cnpj == dto.Cnpj &&
                    c.DataFundacao == dto.DataFundacao
                )),
                Times.Once);
        }

        [Fact]
        public void DeveNotificarCnpjInvalido()
        {
            EmpresaDto dto = new EmpresaDto()
            {
                Id = 0,
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Random.AlphaNumeric(15),
                DataFundacao = DateTime.Now
            };

            _armazenadorDeEmpresa.Armazenar(dto);

            _notificationContextMock.Verify(x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Cnpj inválido"),
                Times.Once);
        }

        [Fact]
        public void DeveNotificarQueDadosDaEmpresaEstaoInvalidos()
        {
            EmpresaDto dto = new EmpresaDto()
            {
                Id = 0,
                Nome = _faker.Random.String2(151),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = DateTime.Now
            };

            _armazenadorDeEmpresa.Armazenar(dto);

            _notificationContextMock.Verify(x =>
                x.AddNotification(TipoDeNotificacao.ErroDeServico, "Empresa inválida"),
                Times.Once);
        }
    }
}
