using OnboardSIGDB1Dominio.Funcionarios.Dtos;

namespace OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos
{
    public interface IArmazenadorDeFuncionario
    {
        void Armazenar(FuncionarioDto dto);
    }
}
