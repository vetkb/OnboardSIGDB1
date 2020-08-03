using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio.FuncionarioDominio.DTO;
using OnboardSIGDB1Dominio.FuncionarioDominio.Interfaces;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        IMapper _mapper;
        readonly IFuncionarioBusiness _iFuncionarioBusiness;

        public FuncionarioController(IFuncionarioBusiness iFuncionarioBusiness, IMapper mapper)
        {
            _iFuncionarioBusiness = iFuncionarioBusiness;
            _mapper = mapper;
        }

        // GET api/funcionario
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Funcionario> funcionarios = _iFuncionarioBusiness.GetTodosFuncionarios();

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FiltroFuncionarioDto filtro)
        {
            List<Funcionario> funcionarios = _iFuncionarioBusiness.GetFuncionarios(filtro.Nome, filtro.Cpf, filtro.DataContratacao);

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Funcionario funcionario = _iFuncionarioBusiness.GetFuncionario(id);

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

            _iFuncionarioBusiness.Post(funcionario);

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

            _iFuncionarioBusiness.Put(funcionario);

            return Ok();
        }

        // DELETE api/funcionario/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _iFuncionarioBusiness.Delete(id);

            return Ok();
        }
    }
}
