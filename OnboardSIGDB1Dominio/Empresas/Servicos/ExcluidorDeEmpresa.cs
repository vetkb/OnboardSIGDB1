using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;

namespace OnboardSIGDB1Dominio.Empresas.Servicos
{
    public class ExcluidorDeEmpresa : IExcluidorDeEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly INotificationContext _notificationContext;

        public ExcluidorDeEmpresa(
            IEmpresaRepositorio empresaRepositorio,
            INotificationContext notificationContext)
        {
            _empresaRepositorio = empresaRepositorio;
            _notificationContext = notificationContext;
        }

        public bool Excluir(int id)
        {
            Empresa empresa = _empresaRepositorio.ObterPorId(id);
            if (empresa == null)
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Empresa não encontrada");
                return false;
            }

            if (empresa.Funcionarios.Count > 0)
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Existem funcionários associados a essa empresa");
                return false;
            }

            _empresaRepositorio.Delete(empresa);

            return true;
        }
    }
}
