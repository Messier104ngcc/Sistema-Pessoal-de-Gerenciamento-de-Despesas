using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models
{
    public class Usuarios
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Digite seu nome completo!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um usuario!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Digite uma senha!")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Confirme a Senha")]
        public string ConfSenha { get; set; }
    }
}
