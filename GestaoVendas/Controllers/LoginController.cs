using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class LoginController : Controller
    {
        #region Index
        public IActionResult Index(string? erro)
        {
            if (erro != null)
            {
                TempData["error"] = erro;
            }
            return View();
        }
        #endregion

        #region POST - Verificar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verificar(LoginModel login)
        {
            LoginDAO obj = new LoginDAO();
            List<VendedorModel> result = obj.validarLogin(login);
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    if (BCrypt.Net.BCrypt.Verify(login.Senha, item.Senha))
                    {
                        HttpContext.Session.SetString("Idvendedor", item.Idvendedor.ToString());
                        HttpContext.Session.SetString("Nome", item.Nome);
                        HttpContext.Session.SetString("Sexo", item.Sexo);
                        HttpContext.Session.SetString("Email", item.Email);
                        HttpContext.Session.SetString("Nivel", item.Nivel);
                        HttpContext.Session.SetString("Foto", (item.Foto != null ?
                            Convert.ToBase64String(item.Foto) : string.Empty));
                        return RedirectToAction("Index", "Vendedor");
                    }
                }
            }
            string erro = "Usuário ou Senha Inválido";
            return RedirectToAction(nameof(Index), new { @erro = erro });
        }
        #endregion
    }
}