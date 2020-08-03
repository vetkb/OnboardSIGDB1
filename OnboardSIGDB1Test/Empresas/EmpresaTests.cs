using Bogus;
using Bogus.Extensions.Brazil;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
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
        public void DeveValidarFalseQuandoNomeForMuitoGrande()
        {
            var empresa = new Empresa(_nomeInvalido, _cnpjTamanhoValido, null);

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData(13)]
        [InlineData(15)]
        public void DeveValidarFalseQuandoTamanhoDoCnpjForDiferenteDe14(int tamanhoInvalido)
        {
            var cnpjInvalido = _faker.Random.String2(tamanhoInvalido);
            var empresa = new Empresa(_nomeValido, cnpjInvalido, null);

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveValidarFalseQuandoNomeEstiverVazio(string nomeVazio)
        {
            var empresa = new Empresa(nomeVazio, _cnpjTamanhoValido, null);

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveValidarFalseQuandoCnpjEstiverVazio(string cnpjVazio)
        {
            var empresa = new Empresa(_nomeValido, cnpjVazio, null);

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }

        [Fact]
        public void DeveValidarFalseQuandoDataFundacaoForMaiorQueDataDeHoje()
        {
            var dataFundacaoInvalida = DateTime.Now.AddDays(1);
            var empresa = new Empresa(_nomeValido, _cnpjTamanhoValido, dataFundacaoInvalida);

            var resultado = empresa.Validar();

            Assert.False(resultado);
        }
    }
}
