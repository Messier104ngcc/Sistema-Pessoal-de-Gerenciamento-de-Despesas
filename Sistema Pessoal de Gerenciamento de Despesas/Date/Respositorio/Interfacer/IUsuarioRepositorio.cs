using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date.Respositorio.Interfacer
{
    public interface IUsuarioRepositorio
    {
        List<Models.Usuarios> BuscarUsuario();

        void CadastrarUsuario(Models.Usuarios usuarios);
    }
}
