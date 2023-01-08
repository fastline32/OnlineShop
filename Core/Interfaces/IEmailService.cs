using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailConfirmationEmail(UserEmailOptions userEmailOptions);
    }
}