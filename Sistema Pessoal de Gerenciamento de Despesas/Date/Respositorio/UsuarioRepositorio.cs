using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date.Respositorio.Interfacer;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date.Respositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly BancoContexto _bancoContexto;

        public UsuarioRepositorio(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        // comando para buscar os dados do banco.
        public List<Models.Usuarios> BuscarUsuario()
        {
            return _bancoContexto.Login.ToList();
        }

        //responsavel por inserir os dados no banco
        public void CadastrarUsuario(Models.Usuarios usuarios)
        {
            _bancoContexto.Login.Add(usuarios); //inseri os dados no banco.
            _bancoContexto.SaveChanges(); //salva os dados no bando.
        }
    }
}
