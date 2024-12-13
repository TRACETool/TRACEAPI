using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace SecurityWebhoook.Lib.Services.EmailServices
{

    public class MailKitEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _username;
        private readonly string _password;
        private readonly string _fromAddress;

        public MailKitEmailService(string smtpServer, int smtpPort, string username, string password, string fromAddress)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _username = username;
            _password = password;
            _fromAddress = fromAddress;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body, bool isHtml = false)
        {
            var message = CreateEmailMessage(toAddress, subject, body, isHtml);

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls); // Connect securely
                    await client.AuthenticateAsync(_username, _password); // Authenticate
                    await client.SendAsync(message); // Send the email
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    throw; // Handle exceptions as needed
                }
                finally
                {
                    await client.DisconnectAsync(true); // Gracefully disconnect
                }
            }
        }

        private MimeMessage CreateEmailMessage(string toAddress, string subject, string body, bool isHtml)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender", _fromAddress));
            email.To.Add(new MailboxAddress("", toAddress));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = isHtml ? body : null, // Set HTML body if isHtml is true
                TextBody = isHtml ? null : body  // Set plain text body if isHtml is false
            };

            email.Body = bodyBuilder.ToMessageBody();
            return email;
        }
    }

}
