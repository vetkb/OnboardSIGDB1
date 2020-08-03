using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio.CargoDominio.DTO;
using OnboardSIGDB1Dominio.CargoDominio.Interfaces;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        IMapper _mapper;
        private readonly ICargoBusiness _iCargoBusiness;

        public CargoController(ICargoBusiness iCargoBusiness, IMapper mapper)
        {
            _iCargoBusiness = iCargoBusiness;
            _mapper = mapper;
        }

        // GET api/cargo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Cargo> cargos = _iCargoBusiness.GetTodosCargos();

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FiltroCargoDto filtro)
        {
            List<Cargo> cargos = _iCargoBusiness.PesquisarCargos(filtro.Descricao);

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Cargo cargo = _iCargoBusiness.GetCargo(id);

            CargoDto cargoDto = _mapper.Map<CargoDto>(cargo);

            return Ok(cargoDto);
        }

        // POST api/cargo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroCargoDto dto)
        {
            Cargo cargo = new Cargo(dto.Descricao);

            _iCargoBusiness.Post(cargo);

            return Ok();
        }

        // PUT api/cargo
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CadastroCargoDto dto)
        {
            Cargo cargo = new Cargo(id, dto.Descricao);

            _iCargoBusiness.Put(cargo);

            return Ok();
        }

        // DELETE api/cargo/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _iCargoBusiness.Delete(id);

            return Ok();
        }

        // GET api/cargo/dropdown
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            List<Cargo> cargos = _iCargoBusiness.GetTodosCargos();

            List<CargoDropdownDto> cargosDto = _mapper.Map<List<CargoDropdownDto>>(cargos);

            return Ok(cargosDto);
        }
    }
}
