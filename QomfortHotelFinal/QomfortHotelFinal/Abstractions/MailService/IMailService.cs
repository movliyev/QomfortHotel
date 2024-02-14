
namespace QomfortHotelFinal.Abstractions.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string emailTo, string subject, string body, bool isHtml = false);
    }
}
