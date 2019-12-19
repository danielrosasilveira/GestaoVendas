using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class VendedorController : Controller
    {
        #region Index
        public IActionResult Index()
        {
            VendedorDAO obj = new VendedorDAO();            
            ViewBag.Lista = obj.listarVendedores();
            return View();
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VendedorModel vendedor)
        {
            if (ModelState.IsValid)
            {                             
                //Encriptando a senha
                vendedor.Senha = BCrypt.Net.BCrypt.HashPassword(vendedor.Senha);

                
                VendedorDAO obj = new VendedorDAO();
                obj.Inserir(vendedor);
                return RedirectToAction(nameof(Index));
                
            }
            return View();
        }
        #endregion

        #region Edit - GET
        public IActionResult Edit(int id)
        {
            VendedorDAO obj = new VendedorDAO();
            ViewBag.Vendedor = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VendedorModel ven)
        {
            if (ModelState.IsValid)
            {
                VendedorDAO obj = new VendedorDAO();
                obj.Editar(ven);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Vendedor = ven;
            return View();
        }
        #endregion

        #region Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            VendedorDAO obj = new VendedorDAO();
            ViewBag.Vendedor = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(VendedorModel ven)
        {
            VendedorDAO obj = new VendedorDAO();
            obj.Excluir(ven.Idvendedor);

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}


