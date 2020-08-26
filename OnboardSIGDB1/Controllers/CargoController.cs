using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : BaseController
    {
        IMapper _mapper;
        private readonly ICargoConsulta _cargoConsulta;
        private readonly IArmazenadorDeCargo _armazenadorDeCargo;
        private readonly IExcluidorDeCargo _excluidorDeCargo;

        public CargoController(
            ICargoConsulta iCargoConsulta, 
            IArmazenadorDeCargo armazenadorDeCargo, 
            IMapper mapper,
            IExcluidorDeCargo excluidorDeCargo,
            INotificationContext notificationContext) : base(notificationContext)
        {
            _cargoConsulta = iCargoConsulta;
            _armazenadorDeCargo = armazenadorDeCargo;
            _mapper = mapper;
            _excluidorDeCargo = excluidorDeCargo;
        }

        // GET api/cargo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Cargo> cargos = _cargoConsulta.ObterTodos();

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FiltroCargoDto filtro)
        {
            List<Cargo> cargos = _cargoConsulta.Pesquisar(filtro);

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Cargo cargo = _cargoConsulta.ObterPorId(id);

            if (cargo == null)
            {
                return NotFound(cargo);
            }

            CargoDto cargoDto = _mapper.Map<CargoDto>(cargo);

            return Ok(cargoDto);
        }

        // POST api/cargo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            _armazenadorDeCargo.Armazenar(dto);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // PUT api/cargo
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            dto.Id = id;

            _armazenadorDeCargo.Armazenar(dto);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // DELETE api/cargo/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = _excluidorDeCargo.Excluir(id);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok(resultado);
        }

        // GET api/cargo/dropdown
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            List<Cargo> cargos = _cargoConsulta.ObterTodos();

            List<DropdownCargoDto> cargosDto = _mapper.Map<List<DropdownCargoDto>>(cargos);

            return Ok(cargosDto);
        }
    }
}
