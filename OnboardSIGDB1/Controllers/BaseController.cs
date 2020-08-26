using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1Dominio._Base.Interfaces;
using System.Linq;

namespace OnboardSIGDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly INotificationContext _notificationContext;

        public BaseController(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected bool OperacaoValida() => !_notificationContext.HasNotifications;

        protected BadRequestObjectResult BadRequestResponse()
        {
            return BadRequest(_notificationContext.Notifications.Select(n => n.Message));
        }
    }
}
