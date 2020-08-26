using OnboardSIGDB1Dominio._Base.Enums;

namespace OnboardSIGDB1Dominio._Base
{
    public class Notification
    {
        public TipoDeNotificacao Key { get; }
        public string Message { get; }
        public Notification(TipoDeNotificacao key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
