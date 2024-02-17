
namespace QomfortHotelFinal.Abstractions.MailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailTo, string subject, string body, bool isHtml = false);
    }
}
