using System;
using System.Collections.Generic;
using System.IO;
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
        //Via Injeção de dependência irá receber automaticamente
        //o vendedor que está logado no sistema
        private IHttpContextAccessor httpContext;
        public LoginController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = HttpContextAccessor;
        }

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

        #region Verificar - POST
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
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            string erro = "Usuário ou Senha Inválido";
            return RedirectToAction(nameof(Index), new { @erro = erro });
        }
        #endregion

        #region Perfil - GET
        [HttpGet]
        public IActionResult Perfil()
        {
            int id = Convert.ToInt32(httpContext.HttpContext.Session.GetString("Idvendedor"));
            VendedorDAO obj = new VendedorDAO();
            ViewBag.Vendedor = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Perfil - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(VendedorModel ven, IList<IFormFile> Image)
        {
            IFormFile uploadedImage = Image.FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (Image.Count > 0)
                {
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    var size = ms.Length;
                    if (ms.Length > 1048576)
                    {
                        ViewBag.Vendedor = ven;
                        TempData["error"] = "Limite 1MB";
                        return View();
                    }
                    ven.Foto = ms.ToArray();
                    ven.ContentType = uploadedImage.ContentType;
                    HttpContext.Session.SetString("Foto", Convert.ToBase64String(ven.Foto));
                }
                VendedorDAO obj = new VendedorDAO();
                obj.Editar(ven);
                return RedirectToAction("Index", "Vendedor");
            }
            ViewBag.Vendedor = ven;
            TempData["error"] = "Limite 1MB";
            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Senha - GET
        [HttpGet]
        public IActionResult Senha()
        {
            return View();
        }
        #endregion

        #region Senha - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Senha(SenhaModel senha)       
        {
            if (ModelState.IsValid)
            {
                LoginModel log = new LoginModel();
                log.Email = httpContext.HttpContext.Session.GetString("Email");

                LoginDAO obj = new LoginDAO();
                List<VendedorModel> result = obj.validarLogin(log);

                foreach (var item in result)
                {
                    if (BCrypt.Net.BCrypt.Verify(senha.SenhaAtual, item.Senha))
                    {
                        SenhaModel objSenha = new SenhaModel();
                        objSenha.Id = Convert.ToInt32(httpContext.HttpContext.Session.GetString("Idvendedor"));
                        objSenha.NovaSenha = BCrypt.Net.BCrypt.HashPassword(senha.NovaSenha);

                        obj.alterarSenha(objSenha);
                        ViewData["message"] = ("Senha Alterada com Sucesso");
                    }
                    else
                    {
                        ViewData["message"] = ("Senha Atual Incorreta");
                    }
                }
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Idvendedor", string.Empty);
            HttpContext.Session.SetString("Nome", string.Empty);
            HttpContext.Session.SetString("Sexo", string.Empty);
            HttpContext.Session.SetString("Email", string.Empty);
            HttpContext.Session.SetString("Nivel", string.Empty);
            HttpContext.Session.SetString("Foto", string.Empty);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}