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
using OnboardSIGDB1Dominio;
using OnboardSIGDB1Dominio.CargoDominio.Business;
using OnboardSIGDB1Dominio.CargoDominio.DTO;
using OnboardSIGDB1Dominio.CargoDominio.Interfaces;
using OnboardSIGDB1Dominio.CargoDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Dominio.CargoDominio.Repositorios;
using OnboardSIGDB1Dominio.EmpresaDominio.Business;
using OnboardSIGDB1Dominio.EmpresaDominio.DTO;
using OnboardSIGDB1Dominio.EmpresaDominio.Interfaces;
using OnboardSIGDB1Dominio.EmpresaDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Dominio.EmpresaDominio.Repositorios;
using OnboardSIGDB1Dominio.FuncionarioDominio.Business;
using OnboardSIGDB1Dominio.FuncionarioDominio.DTO;
using OnboardSIGDB1Dominio.FuncionarioDominio.Interfaces;
using OnboardSIGDB1Dominio.FuncionarioDominio.ModelosDeBancoDeDados;
using OnboardSIGDB1Dominio.FuncionarioDominio.Repositorios;
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

            services.AddScoped<ICargoBusiness, CargoBusiness>();
            services.AddScoped<ICargoRepositorio, CargoRepositorio>();
            services.AddScoped<IEmpresaBusiness, EmpresaBusiness>();
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IFuncionarioBusiness, FuncionarioBusiness>();
            services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDto>();
                cfg.CreateMap<Cargo, CargoDropdownDto>();
                cfg.CreateMap<Empresa, EmpresaDto>();
                cfg.CreateMap<Empresa, EmpresaDropdownDto>();
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
