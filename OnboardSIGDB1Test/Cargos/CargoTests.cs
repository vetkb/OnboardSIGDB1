using Bogus;
using ExpectedObjects;
using OnboardSIGDB1Dominio.Cargos.Builders;
using OnboardSIGDB1Test.Builders;
using Xunit;

namespace OnboardSIGDB1Test.Cargos
{
    public class CargoTests
    {
        private readonly Faker _faker;

        public CargoTests()
        {
            _faker = FakerBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCriarUmCargo()
        {
            var cargoEsperado = new
            {
                Descricao = "teste"
            };

            var cargo = CargoBuilder.Novo().ComDescricao("teste").Build();

            cargoEsperado.ToExpectedObject().ShouldMatch(cargo);
        }

        [Fact]
        public void DeveRetornarFalseQuandoDescricaoForMuitoGrande()
        {
            var descricaoInvalida = _faker.Random.String2(251);

            var cargo = CargoBuilder.Novo().ComDescricao(descricaoInvalida).Build();
            var resultado = cargo.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveRetornarFalseQuandoDescricaoEstiverVaziaOuNula(string descricaoVazia)
        {
            var cargo = CargoBuilder.Novo().ComDescricao(descricaoVazia).Build();

            var resultado = cargo.Validar();

            Assert.False(resultado);
        }
    }
}
