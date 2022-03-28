using System.Net.Mail;
using System.Net;

namespace IMBox.Shared.Infrastructure.EmailClient.EmailSmtpClient
{
    public class SmtpEmailClient : IEmailClient
    {
        readonly private SmtpClient _smtpClient;
        public SmtpEmailClient(SmtpSettings settings)
        {
            _smtpClient = new SmtpClient(settings.Host)
            {
                Port = settings.Port,
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = true,
            };
        }

        public void Send(string recipientEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            message.From = new MailAddress("no-reply@IMBox.com");
            message.To.Add(recipientEmail);

            // only for async?
            _smtpClient.SendCompleted += (s, e) =>
            {
                SmtpClient callbackClient = s as SmtpClient;
                MailMessage callbackMailMessage = e.UserState as MailMessage;
                callbackClient.Dispose();
                callbackMailMessage.Dispose();
            };

            _smtpClient.Send(message);
        }
    }
}