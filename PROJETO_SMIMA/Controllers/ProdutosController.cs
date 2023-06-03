using Microsoft.AspNetCore.Mvc;

namespace PROJETO_SMIMA.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult IndexProduto()
        {
            return View();
        }
    }
}
