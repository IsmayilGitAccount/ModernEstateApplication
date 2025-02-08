namespace ModernEstate.Persistence.Implementations.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public class EmailSender
    {
        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                // SMTP istemcisi oluşturuluyor
                var smtpClient = new SmtpClient("smtp.example.com")
                {
                    Port = 587,  // Port numarasını SMTP sunucusuna göre ayarla (genellikle 587 ya da 465)
                    Credentials = new NetworkCredential("ismayilni-bp217@code.edu.az", "ismayilibrahimzade2005"),
                    EnableSsl = true, // Güvenli bağlantı sağlanması için SSL
                };

                // HTML formatında bir e-posta mesajı oluşturuluyor
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("ismayilni-bp217@code.edu.az"),
                    Subject = subject,
                    Body = body,  // Buraya HTML içeriği ekliyoruz
                    IsBodyHtml = true,  // E-posta içeriği HTML formatında olacak
                };

                // Alıcıyı ekliyoruz
                mailMessage.To.Add(recipientEmail);

                // E-posta gönderimi
                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderme hatası: {ex.Message}");
            }
        }
    }


}
