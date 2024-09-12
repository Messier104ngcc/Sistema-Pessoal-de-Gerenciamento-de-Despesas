using Microsoft.AspNetCore.Mvc;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Controllers
{
    public class DespesasController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult AdicionarDespesas()
        {
            return View();
        }
    }
}
