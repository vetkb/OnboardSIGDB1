using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio.EmpresaDominio.DTO;
using OnboardSIGDB1Dominio.EmpresaDominio.Interfaces;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        IMapper _mapper;
        private readonly IEmpresaBusiness _iEmpresaBusiness;

        public EmpresaController(IEmpresaBusiness iEmpresaBusiness, IMapper mapper)
        {
            _iEmpresaBusiness = iEmpresaBusiness;
            _mapper = mapper;
        }

        // GET api/empresa
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Empresa> empresas = _iEmpresaBusiness.GetTodasEmpresas();

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }        

        // GET api/empresa/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery]FiltroEmpresaDto filtro)
        {
            List<Empresa> empresas = _iEmpresaBusiness.GetEmpresas(filtro.Nome, filtro.Cnpj, filtro.DataFundacao);

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }

        // GET api/empresa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Empresa empresa = _iEmpresaBusiness.GetEmpresa(id);

            EmpresaDto empresaDto = _mapper.Map<EmpresaDto>(empresa);

            if (empresaDto == null)
            {
                return NotFound(empresaDto);
            }

            return Ok(empresaDto);
        }

        // POST api/empresa
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroEmpresaDto dto)
        {
            Empresa empresa = new Empresa(dto.Nome, dto.Cnpj, dto.DataFundacao);

            try
            {
                _iEmpresaBusiness.Post(empresa);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        // PUT api/empresa
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CadastroEmpresaDto dto)
        {
            try
            {
                Empresa empresa = new Empresa(id, dto.Nome, dto.Cnpj, dto.DataFundacao);

                _iEmpresaBusiness.Put(empresa);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        // DELETE api/empresa/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _iEmpresaBusiness.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/empresa/dropdown
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            List<Empresa> empresas = _iEmpresaBusiness.GetTodasEmpresas();

            List<EmpresaDropdownDto> empresaDtos = _mapper.Map<List<EmpresaDropdownDto>>(empresas);

            return Ok(empresaDtos);
        }
    }
}
