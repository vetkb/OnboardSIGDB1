using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Empresas.Builders;
using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Helpers;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.Empresas.Servicos
{
    public class ArmazenadorDeEmpresa : IArmazenadorDeEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly INotificationContext _notificationContext;

        public ArmazenadorDeEmpresa(
            IEmpresaRepositorio empresaRepositorio,
            INotificationContext notificationContext)
        {
            _empresaRepositorio = empresaRepositorio;
            _notificationContext = notificationContext;
        }

        public void Armazenar(EmpresaDto dto)
        {
            if (!CnpjHelper.Valido(dto.Cnpj))
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Cnpj inválido");
                return;
            }

            dto.Cnpj = CnpjHelper.RemoveMascara(dto.Cnpj);

            Empresa empresa;
            if (dto.Id == 0)
            {
                empresa = EmpresaBuilder.Novo()
                    .ComNome(dto.Nome)
                    .ComCnpj(dto.Cnpj)
                    .ComDataFundacao(dto.DataFundacao)
                    .Build();
            }
            else
            {
                empresa = _empresaRepositorio.ObterPorId(dto.Id);
                if (empresa == null)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Empresa não encontrada");
                    return;
                }

                List<Empresa> empresas = _empresaRepositorio.ObterPorFiltro(string.Empty, dto.Cnpj, null);

                if (empresas.Count > 0 && empresas.Any(x => x.Id != empresa.Id))
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "CNPJ utilizado");
                    return;
                }

                empresa.Nome = dto.Nome;
                empresa.Cnpj = dto.Cnpj;
                empresa.DataFundacao = dto.DataFundacao;

                _empresaRepositorio.Put(empresa);
            }

            if (!empresa.Validar())
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Empresa inválida");
                return;
            }

            if (dto.Id == 0)
            {
                List<Empresa> empresas = _empresaRepositorio.ObterPorFiltro(string.Empty, dto.Cnpj, null);

                if (empresas.Count > 0)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "CNPJ utilizado");
                    return;
                }

                _empresaRepositorio.Post(empresa);
            }
        }
    }
}
