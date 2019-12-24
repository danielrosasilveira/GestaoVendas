using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class ClienteController : Controller
    {
        #region Index
        public IActionResult Index()
        {
            ClienteDAO obj = new ClienteDAO();
            ViewBag.Lista = obj.listarClientes();
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
        public IActionResult Create(ClienteModel cli)
        {
            if (ModelState.IsValid)
            {
                ClienteDAO obj = new ClienteDAO();
                obj.Inserir(cli);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Edit - GET
        public IActionResult Edit(int id)
        {
            ClienteDAO obj = new ClienteDAO();
            ViewBag.Cliente = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClienteModel cli)
        {
            if (ModelState.IsValid)
            {
                ClienteDAO obj = new ClienteDAO();
                obj.Editar(cli);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cliente = cli;
            return View();
        }
        #endregion

        #region Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ClienteDAO obj = new ClienteDAO();
            ViewBag.Cliente = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ClienteModel cli)
        {
            ClienteDAO obj = new ClienteDAO();
            obj.Excluir(cli.Idcliente);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}

