using Bogus;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Test.Builders;
using System;
using Xunit;

namespace OnboardSIGDB1Test.Funcionarios
{
    public class FuncionarioTests
    {
        private readonly Faker _faker;
        private readonly string _nomeValido;
        private readonly string _nomeInvalido;
        private readonly string _cpfTamanhoValido;

        public FuncionarioTests()
        {
            _faker = FakerBuilder.Novo().Build();
            _nomeValido = _faker.Random.String2(150);
            _nomeInvalido = _faker.Random.String2(151);
            _cpfTamanhoValido = _faker.Random.String2(11);
        }

        [Fact]
        public void DeveValidarFalseQuandoNomeForMuitoGrande()
        {
            var funcionario = new Funcionario(_nomeInvalido, _cpfTamanhoValido);

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(12)]
        public void DeveValidarFalseQuandoTamanhoDoCpfForDiferenteDe11(int tamanhoInvalido)
        {
            var cpfInvalido = _faker.Random.String2(tamanhoInvalido);
            var funcionario = new Funcionario(_nomeValido, cpfInvalido);

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveValidarFalseQuandoNomeEstiverVazio(string nomeVazio)
        {
            var funcionario = new Funcionario(nomeVazio, _cpfTamanhoValido, null);

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveValidarFalseQuandoCpfEstiverVazio(string cpfVazio)
        {
            var funcionario = new Funcionario(_nomeValido, cpfVazio, null);

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Fact]
        public void DeveValidarFalseQuandoDataContratacaoForMaiorQueDataDeHoje()
        {
            var dataContratacaoInvalida = DateTime.Now.AddDays(1);
            var funcionario = new Funcionario(_nomeValido, _cpfTamanhoValido, dataContratacaoInvalida);

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }
    }
}
