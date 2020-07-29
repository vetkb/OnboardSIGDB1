using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Business;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.DTO;
using OnboardSIGDB1.DTO.Funcionario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        IMapper _mapper;
        private readonly FuncionarioBusiness _funcionarioBusiness;

        public FuncionarioController(FuncionarioBusiness funcionarioBusiness, IMapper mapper)
        {
            _funcionarioBusiness = funcionarioBusiness;
            _mapper = mapper;
        }

        // GET api/funcionario
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Funcionario> funcionarios = _funcionarioBusiness.GetTodosFuncionarios();

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] Filtro filtro)
        {
            List<Funcionario> funcionarios = _funcionarioBusiness.GetFuncionarios(filtro.Nome, filtro.NumeroDocumento, filtro.Data);

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Funcionario funcionario = _funcionarioBusiness.GetFuncionario(id);

            FuncionarioDto funcionarioDto = _mapper.Map<FuncionarioDto>(funcionario);

            return Ok(funcionarioDto);
        }

        // POST api/funcionario
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroFuncionarioDto dto)
        {
            Funcionario funcionario = new Funcionario()
            {
                Nome = dto.Nome,
                DataContratacao = dto.DataContratacao,
                Cpf = dto.Cpf
            };

            _funcionarioBusiness.Post(funcionario);

            return Ok();
        }

        // PUT api/funcionario
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CadastroFuncionarioDto dto)
        {
            Funcionario funcionario = new Funcionario()
            {
                Id = id,
                Nome = dto.Nome,
                DataContratacao = dto.DataContratacao,
                Cpf = dto.Cpf,
                CargoId = dto.CargoId,
                EmpresaId = dto.EmpresaId
            };

            _funcionarioBusiness.Put(funcionario);

            return Ok();
        }

        // DELETE api/funcionario/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _funcionarioBusiness.Delete(id);

            return Ok();
        }
    }
}
