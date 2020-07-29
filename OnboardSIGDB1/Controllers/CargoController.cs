using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Business;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.DTO.Cargo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        IMapper _mapper;
        private readonly CargoBusiness _cargoBusiness;

        public CargoController(CargoBusiness cargoBusiness, IMapper mapper)
        {
            _cargoBusiness = cargoBusiness;
            _mapper = mapper;
        }

        // GET api/cargo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Cargo> cargos = _cargoBusiness.GetTodosCargos();

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] CargoFiltro filtro)
        {
            List<Cargo> cargos = _cargoBusiness.GetCargos(filtro.Descricao);

            List<CargoDto> cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return Ok(cargosDto);
        }

        // GET api/cargo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Cargo cargo = _cargoBusiness.GetCargo(id);

            CargoDto cargoDto = _mapper.Map<CargoDto>(cargo);

            return Ok(cargoDto);
        }

        // POST api/cargo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroCargoDto dto)
        {
            Cargo cargo = new Cargo()
            {
                Descricao = dto.Descricao
            };

            _cargoBusiness.Post(cargo);

            return Ok();
        }

        // PUT api/cargo
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CadastroCargoDto dto)
        {
            Cargo cargo = new Cargo()
            {
                Id = id,
                Descricao = dto.Descricao
            };

            _cargoBusiness.Put(cargo);

            return Ok();
        }

        // DELETE api/cargo/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _cargoBusiness.Delete(id);

            return Ok();
        }
    }
}
