using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnboardSIGDB1.Infra.Data;
using OnboardSIGDB1.Infra.Data.Cargos.Repositorios;
using OnboardSIGDB1.Infra.Data.Empresas.Repositorios;
using OnboardSIGDB1.Infra.Data.Funcionarios.Repositorios;
using OnboardSIGDB1Dominio._Base;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Consultas;
using OnboardSIGDB1Dominio.Cargos.Dtos;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Cargos.Servicos;
using OnboardSIGDB1Dominio.Empresas.Consultas;
using OnboardSIGDB1Dominio.Empresas.Dtos;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Empresas.Servicos;
using OnboardSIGDB1Dominio.Funcionarios.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Dtos;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Funcionarios.Servicos;
using Swashbuckle.AspNetCore.Swagger;

namespace OnboardSIGDB1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "OnboardSIGDB1", Version = "V1" });
            });            

            services.AddDbContext<OnboardContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                , ServiceLifetime.Transient);

            services.AddScoped<ICargoConsulta, CargoConsulta>();
            services.AddScoped<ICargoRepositorio, CargoRepositorio>();
            services.AddScoped<IArmazenadorDeCargo, ArmazenadorDeCargo>();
            services.AddScoped<IExcluidorDeCargo, ExcluidorDeCargo>();

            services.AddScoped<IEmpresaConsulta, EmpresaConsulta>();
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IArmazenadorDeEmpresa, ArmazenadorDeEmpresa>();
            services.AddScoped<IExcluidorDeEmpresa, ExcluidorDeEmpresa>();

            services.AddScoped<IFuncionarioConsulta, FuncionarioConsulta>();
            services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddScoped<IArmazenadorDeFuncionario, ArmazenadorDeFuncionario>();
            services.AddScoped<IExcluidorDeFuncionario, ExcluidorDeFuncionario>();

            services.AddScoped<INotificationContext, NotificationContext>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDto>();
                cfg.CreateMap<Cargo, DropdownCargoDto>();
                cfg.CreateMap<Empresa, EmpresaDto>();
                cfg.CreateMap<Empresa, DropdownEmpresaDto>();
                cfg.CreateMap<Funcionario, FuncionarioDto>()
                .ForMember(dest => dest.EmpresaId, s => s.MapFrom(x => x.EmpresaId))
                .ForMember(dest => dest.EmpresaNome, s => s.MapFrom(x => x.Empresa == null ? string.Empty : x.Empresa.Nome))
                .ForMember(dest => dest.CargoId, s => s.MapFrom(x => x.CargoId))
                .ForMember(dest => dest.CargoDescricao, s => s.MapFrom(x => x.Cargo == null ? string.Empty : x.Cargo.Descricao));
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseCors(options => 
            options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });
        }
    }
}
