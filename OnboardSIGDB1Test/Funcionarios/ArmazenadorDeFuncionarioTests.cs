using Bogus;
using OnboardSIGDB1Dominio._Base.Interfaces;
using OnboardSIGDB1Dominio.Funcionarios.Interfaces.Servicos;
using OnboardSIGDB1Test.Builders;

namespace OnboardSIGDB1Test.Funcionarios
{
    public class ArmazenadorDeFuncionarioTests 
    {
        private readonly IArmazenadorDeFuncionario _armazenadorDeFuncionario;
        private readonly string _nomeValido;
        private readonly Faker _faker;
        private readonly INotificationContext _notificationContext;

        public ArmazenadorDeFuncionarioTests(
            IArmazenadorDeFuncionario armazenadorDeFuncionario,
            INotificationContext notificationContext)
        {
            _faker = FakerBuilder.Novo().Build();
            _nomeValido = _faker.Random.String2(150);
            _armazenadorDeFuncionario = armazenadorDeFuncionario;            
            _notificationContext = notificationContext;
        }

        //[Fact]
        //public void DeveNotificarQuandoCpfForInvalido()
        //{
        //    string cpfInvalido = "88.286.818/0001-00";
        //    string mensagemNaNotificacao = "CPF inválido";

        //    FuncionarioDto dto = new FuncionarioDto
        //    {
        //        Nome = _nomeValido,
        //        Cpf = cpfInvalido
        //    };

        //    _armazenadorDeFuncionario.Armazenar(dto);

        //    Assert.Equal(_notificationContext.Notifications.Select(n => n.Message).FirstOrDefault(), mensagemNaNotificacao);
        //}
    }
}
