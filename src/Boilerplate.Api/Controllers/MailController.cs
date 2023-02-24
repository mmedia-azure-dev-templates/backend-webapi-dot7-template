using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService _mail;
    
    public MailController(IMailService mail)
    {
        _mail = mail;
    }

    [HttpPost("notification")]
    [AllowAnonymous]
    public async Task<ObjectResult> SendNotification(MailStruct mailData)
    {
        WelcomeMailData welcomeMail = new WelcomeMailData()
        {
            Name = "Raúl David Flores Serrano",
            Email = "raul.flores@mad.ec"
        };
        bool result = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

        if (result)
        {
            return StatusCode(StatusCodes.Status200OK, "Mail with attachment has successfully been sent.");
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail with attachment could not be sent.");
        }
    }
}

