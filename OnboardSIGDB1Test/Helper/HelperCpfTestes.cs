using OnboardSIGDB1Dominio.Funcionarios.Helpers;
using Xunit;

namespace OnboardSIGDB1Test.Helper
{
    public class HelperCpfTestes
    {
        [Fact]
        public void DeveRetornarUmaStringCom11CaracteresExatosAposRemoverMascara()
        {
            string cpf = "844.581.470-21";

            string cpfSemMascara = CpfHelper.RemoveMascara(cpf);

            Assert.True(cpfSemMascara.Length == 11);
        }

        [Theory]
        [InlineData("844.581.470-21")]
        [InlineData("651.862.690-93")]
        [InlineData("391.542.910-46")]
        public void DeveApontarQueCpfValido(string cpf)
        {
            Assert.True(CpfHelper.Valido(cpf));
        }

        [Theory]
        [InlineData("123.456.789-01")]
        [InlineData("12345678901")]
        [InlineData("88.286.818/0001-00")]
        [InlineData("88286818000100")]
        [InlineData("aaaa")]
        [InlineData("")]
        [InlineData("123")]
        public void DeveApontarQueCnpjInValido(string cpf)
        {
            Assert.False(CpfHelper.Valido(cpf));
        }
    }
}
