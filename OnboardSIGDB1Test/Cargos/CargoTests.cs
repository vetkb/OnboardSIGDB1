using Bogus;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
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
        public void DeveValidarFalseQuandoDescricaoMuitoGrande()
        {
            var descricaoInvalida = _faker.Random.String2(251);

            var cargo = new Cargo(descricaoInvalida);
            var resultado = cargo.Validar();

            Assert.False(resultado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void DeveValidarFalseQuandoDescricaoEstiverVazia(string descricaoVazia)
        {
            var cargo = new Cargo(descricaoVazia);

            var resultado = cargo.Validar();

            Assert.False(resultado);
        }
    }
}
