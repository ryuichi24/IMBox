namespace IMBox.Shared.Infrastructure.EmailClient
{
    public interface IEmailClient
    {
        void Send(string recipientEmail, string subject, string body);
    }
}