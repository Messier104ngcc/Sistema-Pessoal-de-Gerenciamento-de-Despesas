using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Controllers
{
    [Authorize] //protegendo as paginas internas, onde só será acessivel por usuarios altenticados.
    public class AgendamentoController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
