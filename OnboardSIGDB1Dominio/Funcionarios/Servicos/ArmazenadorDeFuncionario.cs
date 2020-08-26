using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Funcionarios.Builders;
using OnboardSIGDB1Dominio.Funcionarios.Dtos;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Helpers;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.Funcionarios.Servicos
{
    public class ArmazenadorDeFuncionario : IArmazenadorDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly INotificationContext _notificationContext;

        public ArmazenadorDeFuncionario(
            IFuncionarioRepositorio funcionarioRepositorio,
            INotificationContext notificationContext)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _notificationContext = notificationContext;
        }

        public void Armazenar(FuncionarioDto dto)
        {
            if (!CpfHelper.Valido(dto.Cpf))
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "CPF inválido");
                return;
            }            

            dto.Cpf = CpfHelper.RemoveMascara(dto.Cpf);

            Funcionario funcionario;
            if (dto.Id == 0)
            {
                funcionario = FuncionarioBuilder.Novo()
                    .ComNome(dto.Nome)
                    .ComCpf(dto.Cpf)
                    .ComDataContratacao(dto.DataContratacao)
                    .Build();
            }
            else
            {
                funcionario = _funcionarioRepositorio.ObterPorId(dto.Id);
                if (funcionario == null)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Funcionario não encontrado");
                    return;
                }

                List<Funcionario> funcionarios = _funcionarioRepositorio.ObterPorFiltro(string.Empty, dto.Cpf, null);

                if (funcionarios.Count > 0 && funcionarios.Any(x => x.Id != funcionario.Id))
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "CPF utilizado");
                    return;
                }

                funcionario.Nome = dto.Nome;
                funcionario.Cpf = dto.Cpf;
                funcionario.DataContratacao = dto.DataContratacao;
                funcionario.CargoId = dto.CargoId;
                funcionario.EmpresaId = dto.EmpresaId;

                _funcionarioRepositorio.Put(funcionario);
            }

            if (!funcionario.Validar())
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Funcionario inválido");
                return;
            }

            if (dto.Id == 0)
            {
                List<Funcionario> funcionarios = _funcionarioRepositorio.ObterPorFiltro(string.Empty, dto.Cpf, null);

                if (funcionarios.Count > 0)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "CPF utilizado");
                    return;
                }

                _funcionarioRepositorio.Post(funcionario);
            }
        }
    }
}
