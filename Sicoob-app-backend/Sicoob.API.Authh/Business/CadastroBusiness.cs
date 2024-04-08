using Sicoob.API.Authh.Helpers;
using Sicoob.API.Authh.Repository.Interface;

namespace Sicoob.API.Authh.Business
{
    public class CadastroBusiness
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly ISenhaHash _senhaHash;
        public CadastroBusiness(ICadastroRepository cadastroRepository, ISenhaHash senhaHash)
        {
            _cadastroRepository = cadastroRepository;
            _senhaHash = senhaHash;
        }
    }
}
