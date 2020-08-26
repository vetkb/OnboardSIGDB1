using FluentValidation.Results;
using OnboardSIGDB1Dominio._Base.Enums;
using System.Collections.Generic;

namespace OnboardSIGDB1Dominio._Base.Interfaces
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(TipoDeNotificacao key, string message);
        void AddNotification(Notification notification);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(ValidationResult validationResult);
        void CleanNotification();
    }
}
