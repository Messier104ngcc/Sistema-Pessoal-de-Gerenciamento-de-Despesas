using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date.Respositorio.Interfacer;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;
using System.Security.Claims;
using System.Text;

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


        public IActionResult Cadastrar()
        {
            return View("CadastroIndex");
        }

        //public IActionResult CadastrarUsuario(string nome, string usuario, string senha, string confsenha)
        //{
        //    if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confsenha))
        //    {
        //        TempData["MensagemErro"] = "POR FAVOR, PREENCHA TODOS OS CAMPOS";
        //    }
        //    else if (senha != confsenha)
        //    {
        //        TempData["MensagemErro"] = "SENHAS NÃO CORRESPONDEM!";

        //    }
        //    else
        //    {
        //        try
        //        {
        //            _db_Login.Add();
        //            _db_SaveChanges();

        //            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
        //            return RedirectToAction("Index");

        //        }
        //        catch (Exception ex)
        //        {
        //            //ex.Error(ex, "Ocorreu um erro ao processar a requisição.");
        //            //ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        // POST: /Account/Register
        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(Models.Usuarios model)
        {
            if (ModelState.IsValid)
            {
                if (_db.Login.Any(t => model.Senha != model.ConfSenha))
                {
                    ModelState.AddModelError("", "Senhas não coincidem!.");
                    return View("CadastroIndex");
                }

                // Verificar se o usuário já existe
                if (_db.Login.Any(t => t.UserName == model.UserName))
                {
                    ModelState.AddModelError("", "Nome de usuário já existente.");
                    return View("CadastroIndex");
                }
                else
                {
                    _db.Login.Add(model);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Login"); // Redirecionar para a página inicial ou outra página
                }               
            }

            // Se houver erros, reexibir o formulário
            return View("CadastroIndex");
        }
    }
}
