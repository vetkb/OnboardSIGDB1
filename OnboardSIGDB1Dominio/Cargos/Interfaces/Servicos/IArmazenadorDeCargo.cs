using OnboardSIGDB1Dominio.Cargos.Dtos;

namespace OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos
{
    public interface IArmazenadorDeCargo
    {
        void Armazenar(CargoDto cargoDto);
    }
}
