using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnboardSIGDB1.Infra.Data;
using OnboardSIGDB1.Infra.Data.Cargos.Repositorios;
using OnboardSIGDB1.Infra.Data.Empresas.Repositorios;
using OnboardSIGDB1.Infra.Data.Funcionarios.Repositorios;
using OnboardSIGDB1Dominio._Base;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Cargos.Consultas;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Cargos.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Cargos.Servicos;
using OnboardSIGDB1Dominio.Empresas.Consultas;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Empresas.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Empresas.Servicos;
using OnboardSIGDB1Dominio.Funcionarios.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Consultas;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Repositorios;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;
using OnboardSIGDB1Dominio.Funcionarios.Servicos;

namespace OnboardSIGDB1.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, string connection)
        {
            services.AddDbContext<OnboardContext>(options =>
                options.UseSqlServer(connection)
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
        }
    }
}
