using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PROJETO_SMIMA.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace PROJETO_SMIMA.Controllers
{
    
    public class LoginController : Controller
    {
        private Contexto db;
        public LoginController(Contexto _db)
        {
            db = _db;
        }
        public IActionResult IndexLogin()
        {
            return View();
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string senha)
        {
            string senhaString = senha.ToString();
            Usuario pessoa = db.USUARIOS.Where(a => a.Login == login && a.Senha == senha).FirstOrDefault();
            if (pessoa == null)
            {
                TempData["erro"] = "Erro no Login ou Senha";
                return View();
            }
            else
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, pessoa.Login));
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
             


                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index","Home");
            }
        }
    }
}
