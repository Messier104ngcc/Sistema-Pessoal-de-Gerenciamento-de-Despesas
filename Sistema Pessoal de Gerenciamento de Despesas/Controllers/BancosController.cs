using Microsoft.AspNetCore.Mvc;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Controllers
{
    public class BancosController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
