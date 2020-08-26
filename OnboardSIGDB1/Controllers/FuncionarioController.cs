using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Funcionarios.Dtos;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : BaseController
    {
        IMapper _mapper;
        private readonly IFuncionarioConsulta _funcionarioConsulta;
        private readonly IArmazenadorDeFuncionario _armazenadorDeFuncionario;
        private readonly IExcluidorDeFuncionario _excluidorDeFuncionario;

        public FuncionarioController(
            IFuncionarioConsulta funcionarioConsulta,
            IArmazenadorDeFuncionario armazenadorDeFuncionario,
            IExcluidorDeFuncionario excluidorDeFuncionario,
            IMapper mapper,
            INotificationContext notificationContext) : base(notificationContext)
        {
            _funcionarioConsulta = funcionarioConsulta;
            _armazenadorDeFuncionario = armazenadorDeFuncionario;
            _excluidorDeFuncionario = excluidorDeFuncionario;
            _mapper = mapper;
        }

        // GET api/funcionario
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Funcionario> funcionarios = _funcionarioConsulta.ObterTodos();

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FiltroFuncionarioDto filtro)
        {
            List<Funcionario> funcionarios = _funcionarioConsulta.ObterPorFiltro(filtro);

            List<FuncionarioDto> funcionariosDto = _mapper.Map<List<FuncionarioDto>>(funcionarios);

            return Ok(funcionariosDto);
        }

        // GET api/funcionario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Funcionario funcionario = _funcionarioConsulta.ObterPorId(id);

            if (funcionario == null)
            {
                return NotFound(funcionario);
            }

            FuncionarioDto funcionarioDto = _mapper.Map<FuncionarioDto>(funcionario);

            return Ok(funcionarioDto);
        }

        // POST api/funcionario
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroFuncionarioDto dto)
        {
            FuncionarioDto funcionario = new FuncionarioDto()
            {
                Nome = dto.Nome,
                DataContratacao = dto.DataContratacao,
                Cpf = dto.Cpf
            };

            _armazenadorDeFuncionario.Armazenar(funcionario);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // PUT api/funcionario
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            dto.Id = id;

            _armazenadorDeFuncionario.Armazenar(dto);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // DELETE api/funcionario/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool resultado = _excluidorDeFuncionario.Excluir(id);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok(resultado);
        }
    }
}
