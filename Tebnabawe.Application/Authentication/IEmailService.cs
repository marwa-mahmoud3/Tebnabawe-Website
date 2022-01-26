using System.Threading.Tasks;

namespace Tebnabawe.Application.Authentication
{
    public interface IEmailService
    {
         void SendEmail(string toEmail, string subject, string content);
    }
}
