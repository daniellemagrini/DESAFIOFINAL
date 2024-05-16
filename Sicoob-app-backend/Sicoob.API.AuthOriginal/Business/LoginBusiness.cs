using Microsoft.AspNetCore.Mvc;
using Sicoob.API.AuthOriginal.Model;
using Sicoob.API.AuthOriginal.Repository.Interface;
using Sicoob.Mail;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sicoob.API.AuthOriginal.Business
{
    public class LoginBusiness
    {
        private readonly ILoginRepository _loginRepository;
        public LoginBusiness(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public void EnviarEmailPrimeiroAcesso(string nome, string login, string email, Bitmap qrCode)
        {
            var qrCodeBase64 = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                qrCode.Save(ms, ImageFormat.Png);
                byte[] qrCodeBytes = ms.ToArray();
                qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
            }

            var destinatario = email;
            var assunto = "Primeiro Acesso - Sicoob";
            var corpoEmail = $@"
            <html>
            <head>
                <title>Primeiro Acesso ao Sistema Sicoob</title>
            </head>
            <body>
                <h1>Olá! { nome }</h1>
                <p>Segue abaixo o seu login para acessar o sistema:</p>
                <p><strong>Login:</strong> { login }</p>
                <p>Use o QR Code abaixo para acessar o sistema:</p>
                <br><br><img src='data:image/png;base64,{ qrCodeBase64 }' alt='QR Code'>
                <a href='https://www.exemplo.com/login'>Acessar o Sistema</a>
                <br><br><h3>Time Sicoob</h3>
            </body>
            </html>";

            Mail.Mail.SendEmail(destinatario, assunto, corpoEmail);
        }
    }
}
