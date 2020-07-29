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
using OnboardSIGDB1.Business;
using OnboardSIGDB1.DAL;
using OnboardSIGDB1.DAL.Models;
using OnboardSIGDB1.DTO;
using OnboardSIGDB1.DTO.Cargo;
using OnboardSIGDB1.DTO.Funcionario;
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

            services.AddScoped<EmpresaBusiness>();
            services.AddScoped<FuncionarioBusiness>();
            services.AddScoped<CargoBusiness>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDto>();
                cfg.CreateMap<Empresa, EmpresaDto>();
                cfg.CreateMap<Funcionario, FuncionarioDto>()
                .ForMember(dest => dest.EmpresaNome, s => s.MapFrom(x => x.Empresa == null ? string.Empty : x.Empresa.Nome))
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

            app.UseCors(options => options.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });
        }
    }
}
