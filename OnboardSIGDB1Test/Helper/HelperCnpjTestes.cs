using OnboardSIGDB1Dominio.Empresas.Helpers;
using Xunit;

namespace OnboardSIGDB1Test
{    
    public class HelperCnpjTestes
    {
        [Fact]
        public void DeveRetornarUmaStringCom14CaracteresExatosAposRemoverMascara()
        {
            string cnpj = "87.375.265/0001-52";

            string cnpjSemMascara = CnpjHelper.RemoveMascara(cnpj);

            Assert.True(cnpjSemMascara.Length == 14);
        }

        [Theory]
        [InlineData("87.375.265/0001-52")]
        [InlineData("70.764.826/0001-02")]
        [InlineData("88.286.818/0001-63")]
        public void DeveApontarQueCnpjValido(string cnpj)
        {
            Assert.True(CnpjHelper.Valido(cnpj));
        }

        [Theory]
        [InlineData("123.456.789-01")]
        [InlineData("12345678901")]
        [InlineData("88.286.818/0001-00")]
        [InlineData("88286818000100")]
        [InlineData("aaaa")]
        [InlineData("")]
        [InlineData("123")]
        public void DeveApontarQueCnpjInValido(string cnpj)
        {
            Assert.False(CnpjHelper.Valido(cnpj));
        }
    }
}
