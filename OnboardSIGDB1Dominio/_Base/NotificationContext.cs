using FluentValidation.Results;
using OnboardSIGDB1Dominio._Base.Enums;
using OnboardSIGDB1Dominio._Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1Dominio._Base
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();
        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }
        public void AddNotification(TipoDeNotificacao key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }
        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(ICollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(TipoDeNotificacao.ErroDeDominio, error.ErrorMessage);
            }
        }
        public void CleanNotification()
        {
            _notifications.Clear();
        }
    }
}
