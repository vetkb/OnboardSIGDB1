using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;

namespace OnboardSIGDB1Dominio.Funcionarios.Servicos
{
    public class ExcluidorDeFuncionario : IExcluidorDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly INotificationContext _notificationContext;

        public ExcluidorDeFuncionario(
            IFuncionarioRepositorio funcionarioRepositorio,
            INotificationContext notificationContext)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _notificationContext = notificationContext;
        }

        public bool Excluir(int id)
        {
            Funcionario funcionario = _funcionarioRepositorio.ObterPorId(id);
            if (funcionario == null)
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Funcionário não encontrado");
                return false;
            }

            _funcionarioRepositorio.Delete(funcionario);

            return true;
        }
    }
}
