using OtpNet;
using QRCoder.Core;
using Sicoob.API.AuthOriginal.Data;
using Sicoob.API.AuthOriginal.Repository.Interface;
using System.Drawing;

namespace Sicoob.API.AuthOriginal.Repository
{
    public class OTPGenerateRepository : IOTPGenerateRepository
    {
        private readonly AppDbContext _context;

        public OTPGenerateRepository(AppDbContext context)
        {
            _context = context;
        }

        public string GerarSecretKey(string email)
        {
            // Defina uma chave secreta (normalmente associada ao usuário)
            //string secretKey = "hBkERfTpXqZwYmDnScAvL";
            var key = KeyGeneration.GenerateRandomKey(20);
            var secretKey = Base32Encoding.ToString(key);

            //URI para cadastrar no QR Code
            var uriString = new OtpUri(OtpType.Totp, secretKey, email, "Acelera.NET").ToString();
            return uriString;
        }

        public Bitmap CriarQRCode(string uriString)
        {
            // Crie um QR code para o token
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(uriString, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10); // Tamanho do QR code
            return qrCodeImage;
        }

        public bool VerificaSecretKey(string secretKey, string codigo)
        {
            // Crie um gerador TOTP com base na chave secreta
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));

            // Ler o OTP digitado pelo usuário
            var userOtp = codigo;

            // Verificar se o OTP é válido
            bool isValidOtp = totp.VerifyTotp(userOtp, out long timeStepMatched, window: null);

            if (isValidOtp) return true; return false;
        }
    }
}
