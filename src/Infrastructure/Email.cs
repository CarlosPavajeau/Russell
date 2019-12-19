using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Entity;

namespace Infrastructure
{
    public class Email
    {
        private MailMessage MailMessage;
        private readonly SmtpClient Client;

        public Email()
        {
            Client = new SmtpClient();
        }

        public bool SendEmail(Employee employee)
        {
            try
            {
                ConfigSMTP();
                ConfigEmail(employee);
                Client.SendMailAsync(MailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ConfigSMTP()
        {
            Client.Host = "smtp.gmail.com";
            Client.Port = 587;
            Client.EnableSsl = true;
            Client.UseDefaultCredentials = false;

            Client.Credentials = new NetworkCredential("feres223ger@gmail.com", "olakease123");
        }

        private void ConfigEmail(Employee employee)
        {
            MailMessage = new MailMessage();
            MailMessage.To.Add(employee.Email);
            MailMessage.From = new MailAddress("feres223ger@gmail.com");

            MailMessage.Subject = $"Bienvenido a la empresa. Señor {employee.Name}.";
            MailMessage.Body = $"Usted acaba de ser registrado como uno de nuestros empleados el dia {DateTime.Now.ToShortDateString()}";
            MailMessage.IsBodyHtml = false;
            MailMessage.Priority = MailPriority.High;
        }
    }
}
