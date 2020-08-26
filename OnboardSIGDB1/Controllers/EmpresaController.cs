using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : BaseController
    {
        IMapper _mapper;
        private readonly IArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private readonly IExcluidorDeEmpresa _excluidorDeEmpresa;
        private readonly IEmpresaConsulta _empresaConsulta;

        public EmpresaController(
            IArmazenadorDeEmpresa armazenadorDeEmpresa,
            IExcluidorDeEmpresa excluidorDeEmpresa,
            IEmpresaConsulta empresaConsulta,
            IMapper mapper,
            INotificationContext notificationContext) : base(notificationContext)
        {
            _armazenadorDeEmpresa = armazenadorDeEmpresa;
            _excluidorDeEmpresa = excluidorDeEmpresa;
            _empresaConsulta = empresaConsulta;
            _mapper = mapper;
        }

        // GET api/empresa
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Empresa> empresas = _empresaConsulta.ObterTodas();

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }

        // GET api/empresa/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FiltroEmpresaDto filtro)
        {
            List<Empresa> empresas = _empresaConsulta.ObterPorFiltro(filtro);

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }

        // GET api/empresa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Empresa empresa = _empresaConsulta.ObterPorId(id);

            if (empresa == null)
            {
                return NotFound(empresa);
            }

            EmpresaDto empresaDto = _mapper.Map<EmpresaDto>(empresa);

            return Ok(empresaDto);
        }

        // POST api/empresa
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            _armazenadorDeEmpresa.Armazenar(dto);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // PUT api/empresa
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            dto.Id = id;

            _armazenadorDeEmpresa.Armazenar(dto);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok();
        }

        // DELETE api/empresa/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool resultado = _excluidorDeEmpresa.Excluir(id);

            if (!OperacaoValida())
            {
                return BadRequestResponse();
            }

            return Ok(resultado);
        }

        // GET api/empresa/dropdown
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            List<Empresa> empresas = _empresaConsulta.ObterTodas();

            List<DropdownEmpresaDto> empresaDtos = _mapper.Map<List<DropdownEmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }
    }
}
