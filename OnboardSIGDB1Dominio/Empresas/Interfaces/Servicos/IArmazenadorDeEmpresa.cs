using OnboardSIGDB1Dominio.Empresas.Dtos;

namespace OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos
{
    public interface IArmazenadorDeEmpresa
    {
        void Armazenar(EmpresaDto dto);
    }
}
