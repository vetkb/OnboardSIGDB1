using Bogus;
using OnboardSIGDB1Dominio.Funcionarios.Builders;
using OnboardSIGDB1Test.Builders;
using System;
using Xunit;

namespace OnboardSIGDB1Test.Funcionarios
{
    public class FuncionarioTests
    {
        private readonly Faker _faker;
        private readonly string _nomeTamanhoValido;
        private readonly string _nomeTamanhoInvalido;
        private readonly string _cpfTamanhoValido;

        public FuncionarioTests()
        {
            _faker = FakerBuilder.Novo().Build();
            _nomeTamanhoValido = _faker.Random.String2(150);
            _nomeTamanhoInvalido = _faker.Random.String2(151);
            _cpfTamanhoValido = _faker.Random.String2(11);
        }

        [Fact]
        public void DeveRetornarFalseQuandoNomeForMuitoGrande()
        {
            var funcionario = FuncionarioBuilder.Novo()
                .ComNome(_nomeTamanhoInvalido)
                .Build();

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveRetornarFalseQuandoNomeEstiverVazio(string nomeVazio)
        {
            var funcionario = FuncionarioBuilder.Novo()
                .ComNome(nomeVazio)
                .Build();

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(12)]
        public void DeveRetornarFalseQuandoTamanhoDoCpfForDiferenteDe11(int cpfTamanhoInvalido)
        {
            var cpfInvalido = _faker.Random.String2(cpfTamanhoInvalido);
            var funcionario = FuncionarioBuilder.Novo()
                .ComNome(_nomeTamanhoValido)
                .ComCpf(cpfInvalido)
                .Build();

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }        

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveRetornarFalseQuandoCpfEstiverVazio(string cpfVazio)
        {
            var funcionario = FuncionarioBuilder.Novo()
                .ComNome(_nomeTamanhoValido)
                .ComCpf(cpfVazio)
                .Build();

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }

        [Fact]
        public void DeveRetornarFalseQuandoDataContratacaoForMaiorQueDataDeHoje()
        {
            var dataContratacaoInvalida = DateTime.Now.AddDays(1);
            var funcionario = FuncionarioBuilder.Novo()
                .ComNome(_nomeTamanhoValido)
                .ComCpf(_cpfTamanhoValido)
                .ComDataContratacao(dataContratacaoInvalida)
                .Build();

            var resultado = funcionario.Validar();

            Assert.False(resultado);
        }
    }
}
