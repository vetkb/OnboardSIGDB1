﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Business;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.DTO;
using OnboardSIGDB1.DTO.Empresa;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        IMapper _mapper;
        private readonly EmpresaBusiness _empresaBusiness;

        public EmpresaController(EmpresaBusiness empresaBusiness, IMapper mapper)
        {
            _empresaBusiness = empresaBusiness;
            _mapper = mapper;
        }

        // GET api/empresa
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Empresa> empresas = _empresaBusiness.GetTodasEmpresas();

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }

        // GET api/empresa/pesquisar
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery]Filtro filtro)
        {
            List<Empresa> empresas = _empresaBusiness.GetEmpresas(filtro.Nome, filtro.NumeroDocumento, filtro.Data);

            List<EmpresaDto> empresaDtos = _mapper.Map<List<EmpresaDto>>(empresas);

            return Ok(empresaDtos);
        }

        // GET api/empresa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Empresa empresa = _empresaBusiness.GetEmpresa(id);

            EmpresaDto empresaDto = _mapper.Map<EmpresaDto>(empresa);

            return Ok(empresaDto);
        }

        // POST api/empresa
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastroEmpresaDto dto)
        {
            Empresa empresa = new Empresa()
            {
                Nome = dto.Nome,
                DataFundacao = dto.DataFundacao,
                Cnpj = dto.Cnpj
            };

            _empresaBusiness.Post(empresa);

            return Ok();
        }

        // PUT api/empresa
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CadastroEmpresaDto dto)
        {
            Empresa empresa = new Empresa()
            {
                Id = id,
                Nome = dto.Nome,
                DataFundacao = dto.DataFundacao,
                Cnpj = dto.Cnpj
            };

            _empresaBusiness.Put(empresa);

            return Ok();
        }

        // DELETE api/empresa/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _empresaBusiness.Delete(id);

            return Ok();
        }
    }
}