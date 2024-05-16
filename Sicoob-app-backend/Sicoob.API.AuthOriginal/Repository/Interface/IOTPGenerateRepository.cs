using System.Drawing;

namespace Sicoob.API.AuthOriginal.Repository.Interface
{
    public interface IOTPGenerateRepository
    {
        string GerarSecretKey(string email);
        Bitmap CriarQRCode(string uriString);
        bool VerificaSecretKey(string secretKey, string codigo);

    }
}
