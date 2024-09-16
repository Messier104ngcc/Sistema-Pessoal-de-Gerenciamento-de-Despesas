using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date.Respositorio.Interfacer;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;
using System.Security.Claims;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private BancoContexto _db;

        public LoginController(BancoContexto db)
        {
            _db = db;
        }

        // Exibir a página de login
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        // Processa o login do usuário
        [HttpPost]
        public async Task<IActionResult> Entrar(string username, string senha)
        {
            // Verificar se o usuário existe no banco de dados
            var usuarioExistente = _db.Login
                .Where(t => t.UserName == username && t.Senha == senha)
                .FirstOrDefault();

            if (usuarioExistente != null)
            {
                // Criar as claims de identidade do usuário
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Usuario")  // Defina um papel (role) conforme necessário
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Criar o cookie de autenticação
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // O cookie permanece após o fechamento do navegador
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                // Redirecionar para a página inicial do sistema após login
                TempData["MensagemSucesso"] = "Usuario encontrato!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Exibe a mensagem de erro se o login for inválido
                TempData["MensagemErro"] = "Usuário ou senha inválidos!";
                return View("Index");
            }
         }

        // Logout do usuário
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult CadastrarUsuario(Models.Usuarios login) // classe Aluno e obejeto que referencia a classe aluno.
        {
            //
            try
            {
                _usuarioRepositorio.CadastrarUsuario(login);
            }
            catch (Exception ex)
            {
                //ex.Error(ex, "Ocorreu um erro ao processar a requisição.");
                //ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }

            return RedirectToAction("Index");
        }
    }
}
