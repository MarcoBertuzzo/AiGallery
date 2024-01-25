namespace AiGallery.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public class EmailResult
    {
        public int result { get; set; }
        public string description { get; set; }
    }

    public class SmtpClientConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailAddress { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class EmailService
    {
        private readonly SmtpClientConfiguration? SmtpClientConfig;
        public EmailService(IConfiguration configuration)
        {
            SmtpClientConfig = configuration.GetSection("AiGallery:SmtpClient").Get<SmtpClientConfiguration>(); 
        }

        public EmailResult SendEmail(string toAddress, string subject, string body)
        {
            if (SmtpClientConfig == null || string.IsNullOrEmpty(SmtpClientConfig.Username) 
                || string.IsNullOrEmpty(SmtpClientConfig.Host) || string.IsNullOrEmpty(SmtpClientConfig.MailAddress))
                return new EmailResult() { result = 0, description = "Smtp not configured!" };

            if (string.IsNullOrEmpty(toAddress))
                toAddress = SmtpClientConfig.MailAddress;

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = SmtpClientConfig.Port,
                    Credentials = new NetworkCredential(SmtpClientConfig.Username, SmtpClientConfig.Password),
                    EnableSsl = SmtpClientConfig.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(SmtpClientConfig.MailAddress),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toAddress);

                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
                return new EmailResult() { result = 1, description = "OK" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return new EmailResult() { result = 0, description = ex.Message };
            }
        }
    }
}
