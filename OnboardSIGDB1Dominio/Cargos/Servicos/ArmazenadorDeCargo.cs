using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Builders;
using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio.Cargos.Servicos
{
    public class ArmazenadorDeCargo : IArmazenadorDeCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly INotificationContext _notificationContext;

        public ArmazenadorDeCargo(
            ICargoRepositorio cargoRepositorio,
            INotificationContext notificationContext)
        {
            _cargoRepositorio = cargoRepositorio;
            _notificationContext = notificationContext;
        }

        public void Armazenar(CargoDto dto)
        {
            Cargo cargo;
            if (dto.Id == 0)
            {
                cargo = CargoBuilder.Novo().ComDescricao(dto.Descricao).Build();
            }
            else
            {
                cargo = _cargoRepositorio.ObterPorId(dto.Id);
                if (cargo == null)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo não encontrado");
                    return;
                }

                List<Cargo> cargos = _cargoRepositorio.ObterPorDescricao(dto.Descricao);

                if (cargos.Count > 0 && cargos.Any(x => x.Id != cargo.Id))
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Descrição utilizada");
                    return;
                }

                cargo.Descricao = dto.Descricao;

                _cargoRepositorio.Put(cargo);
            }

            if (!cargo.Validar())
            {
                _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Cargo inválido");
                return;
            }

            if (dto.Id == 0)
            {
                List<Cargo> cargos = _cargoRepositorio.ObterPorDescricao(dto.Descricao);

                if (cargos.Count > 0)
                {
                    _notificationContext.AddNotification(TipoDeNotificacao.ErroDeServico, "Descrição utilizada");
                    return;
                }

                _cargoRepositorio.Post(cargo);
            }
        }
    }
}
