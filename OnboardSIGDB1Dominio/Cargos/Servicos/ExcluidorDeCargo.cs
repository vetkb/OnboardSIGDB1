using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;

namespace OnboardSIGDB1Dominio.Cargos.Servicos
{
    public class ExcluidorDeCargo : IExcluidorDeCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly INotificationContext _notificationContext;

        public ExcluidorDeCargo(
            ICargoRepositorio cargoRepositorio,
            INotificationContext notificationContext)
        {
            _cargoRepositorio = cargoRepositorio;
            _notificationContext = notificationContext;
        }

        public bool Excluir(int id)
        {
            var cargo = _cargoRepositorio.ObterPorId(id);
            if (cargo == null)
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo não encontrado");
                return false;
            }

            if (cargo.Funcionarios.Count > 0)
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Existem funcionários associados ao cargo");
                return false;
            }

            _cargoRepositorio.Delete(cargo);

            return true;
        }
    }
}
