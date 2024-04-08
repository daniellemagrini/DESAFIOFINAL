using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Sicoob.Mail
{
    public static class Mail
    {
        static readonly string EmailHost = "smtp-mail.outlook.com";
        static readonly string EmailUsername = "sicoob-codigo-verificacao@hotmail.com";
        static readonly string EmailPassword = "sicoob123";

        public static bool SendEmail(string destinatario,string emailAssunto, string emailCorpo)
        {
            var retorno = false;

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(EmailUsername));
                email.To.Add(MailboxAddress.Parse(destinatario));
                email.Subject = emailAssunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailCorpo
                };

                using var smtp = new SmtpClient();
                smtp.Connect(EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(EmailUsername, EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);

                retorno = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível enviar o email.");
            }

            return retorno;
        }
    }
}