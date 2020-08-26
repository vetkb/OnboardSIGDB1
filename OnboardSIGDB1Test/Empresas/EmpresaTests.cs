using Bogus;
using Bogus.Extensions.Brazil;
using OnboardSIGDB1Dominio.Empresas.Builders;
using OnboardSIGDB1Test.Builders;
using System;
using Xunit;

namespace OnboardSIGDB1Test
{
    public class EmpresaTests
    {
        private readonly Faker _faker;
        private readonly string _nomeValido;
        private readonly string _nomeInvalido;
        private readonly string _cnpjTamanhoValido;

        public EmpresaTests()
        {
            _faker = FakerBuilder.Novo().Build();
            _nomeValido = _faker.Random.String2(150);
            _nomeInvalido = _faker.Random.String2(151);
            _cnpjTamanhoValido = _faker.Company.Cnpj(false);
        }

        [Fact]
        public void DeveRetornarTrueSemDataDeFundacao()
        {
            var empresa = EmpresaBuilder.Novo().ComNome(_nomeValido).ComCnpj(_cnpjTamanhoValido).Build();

            var resultado = empresa.Validar();

            Assert.True(resultado);
        }

        [Fact]
        public void DeveRetornarTrueComDataDeFundacao()
        {
            var dataValida = DateTime.Now.Date;
            var empresa = EmpresaBuilder.Novo()
                .ComNome(_nomeValido)
                .ComCnpj(_cnpjTamanhoValido)
                .ComDataFundacao(dataValida)
                .Build();

            var resultado = empresa.Validar();

            Assert.True(resultado);
        }

        [Fact]
        public void DeveRetornarFalseQuandoNomeForMuitoGrande()
        {
            var empresa = EmpresaBuilder.Novo().ComNome(_nomeInvalido).Build();

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveRetornarFalseQuandoNomeEstiverVazio(string nomeVazio)
        {
            var empresa = EmpresaBuilder.Novo().ComNome(nomeVazio).Build();

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData(13)]
        [InlineData(15)]
        public void DeveRetornarFalseQuandoTamanhoDoCnpjForDiferenteDe14(int tamanhoInvalido)
        {
            var cnpjInvalido = _faker.Random.String2(tamanhoInvalido);
            var empresa = EmpresaBuilder.Novo().ComNome(_nomeValido).ComCnpj(cnpjInvalido).Build();

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveRetornarFalseQuandoCnpjEstiverVazio(string cnpjVazio)
        {
            var empresa = EmpresaBuilder.Novo().ComNome(_nomeValido).ComCnpj(cnpjVazio).Build();

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Fact]
        public void DeveRetornarFalseQuandoDataFundacaoForMaiorQueDataDeHoje()
        {
            var dataFundacaoInvalida = DateTime.Now.AddDays(1);
            var empresa = EmpresaBuilder.Novo()
                .ComNome(_nomeValido)
                .ComCnpj(_cnpjTamanhoValido)
                .ComDataFundacao(dataFundacaoInvalida)
                .Build();

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }
    }
}
