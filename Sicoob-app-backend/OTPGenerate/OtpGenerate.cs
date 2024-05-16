using OtpNet;
using QRCoder.Core;
using System.Drawing;
using System.Reflection.Emit;

class OTPGenerate
{
    static void QRcodeGenerate()
    {
        // Defina uma chave secreta (normalmente associada ao usuário)
        //string secretKey = "hBkERfTpXqZwYmDnScAvL";
        var key = KeyGeneration.GenerateRandomKey(20);
        var secretKey = Base32Encoding.ToString(key);

        //URI para cadastrar no QR Code
        var uriString = new OtpUri(OtpType.Totp, secretKey, "thiago@teste.com", "Acelera.NET").ToString();
        Console.WriteLine(uriString);

        // Crie um QR code para o token
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(uriString, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(10); // Tamanho do QR code

        //Gera o QRCode em SVG
        //SvgQRCode qrCode = new SvgQRCode(qrCodeData);
        //string qrCodeAsSvg = qrCode.GetGraphic(20);

        // Salve o QR code como imagem (opcional)
        qrCodeImage.Save($"D:\\OneDrive - ATOS\\ATOS\\Desktop\\OTP\\OTPGenerate\\OTPGenerate\\images\\qrcode_{secretKey}.png");

        Console.WriteLine($"\nQRCode gerado com sucesso: qrcode_{secretKey}.png");
        Console.ReadLine();

        while (true)
        {
            // Crie um gerador TOTP com base na chave secreta
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));

            // Gere um token OTP com base no tempo atual
            //string otp = totp.ComputeTotp();

            //Console.WriteLine($"Token OTP gerado: {otp}");
            //Console.ReadLine();

            // Ler o OTP digitado pelo usuário
            Console.Write("\nDigite o OTP: ");
            var userOtp = Console.ReadLine();

            // Verificar se o OTP é válido
            bool isValidOtp = totp.VerifyTotp(userOtp, out long timeStepMatched, window: null);

            if (isValidOtp)
            {
                Console.WriteLine("OTP válido!");
            }
            else
            {
                Console.WriteLine("OTP inválido.");
            }
        }
    }
}