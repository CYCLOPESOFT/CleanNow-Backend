using CleanNow.Core.Application.Dto.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Interfaces.Shared
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
    }
}
