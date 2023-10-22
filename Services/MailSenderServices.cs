
using Microsoft.AspNetCore.Identity.UI.Services;

public class MailSenderServices : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return null;
    }
}