namespace Sicoob.API.Authh.Helpers
{
    public interface ISenhaHash
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(int id, string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
