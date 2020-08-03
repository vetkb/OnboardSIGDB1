using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using Xunit;

namespace OnboardSIGDB1Test
{
    public class CargoTestes
    {
        public void DeveCriarUmCargoComDescricaoSomente()
        {
            string descricao = "teste";
            Cargo cargo = new Cargo(descricao);

            Assert.Equal(descricao, cargo.Descricao);
        }

        public void DeveCriarUmCargoComTodosOsCamposPreenchidos()
        {
            int id = 1;
            string descricao = "teste";
            Cargo cargo = new Cargo(id, descricao);

            Assert.Equal(id, cargo.Id);
            Assert.Equal(descricao, cargo.Descricao);
        }
    }
}
