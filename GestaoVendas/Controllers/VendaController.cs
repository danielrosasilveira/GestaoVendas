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
    public class VendaController : Controller
    {
        //Via Injeção de dependência a venda irá receber automaticamente
        //o vendedor que está logado no sistema
        private IHttpContextAccessor httpContext;
        public VendaController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = HttpContextAccessor;
        }

        #region Index
        public IActionResult Index()
        {
            VendaDAO obj = new VendaDAO();
            ViewBag.Lista = obj.listarVendas();
            return View();
        }
        #endregion

        #region Create - GET
        public IActionResult Create()
        {
            CarregarDados();
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VendaModel venda)
        {
            CarregarDados();
            if (venda.ListaProdutosJSON != null)
            {
                //Trabalhando Contexto do Vendedor
                //pegando a variável de sessão que foi criada na HomeController
                venda.Vendedor = httpContext.HttpContext.Session.GetString("Idvendedor");

                VendaDAO obj = new VendaDAO();
                obj.Inserir(venda);

                ViewData["message"] = ("OK");                               
                return View(nameof(Create));
            }
            return View();
        }
        #endregion

        #region CarregarDados
        private void CarregarDados()
        {
            ViewBag.ListaClientes = new ClienteDAO().listarClientes();
            ViewBag.ListaVendedores = new VendedorDAO().listarVendedores();
            ViewBag.ListaProdutos = new ProdutoDAO().listarProdutos();
        }
        #endregion

        #region Sucesso
        public IActionResult Sucesso()
        {
            return View();
        }
        #endregion

        #region Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            VendaDAO obj = new VendaDAO();
            ViewBag.Venda = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VendaModel ven)
        {
            VendaDAO obj = new VendaDAO();
            obj.Excluir(ven.Idvenda);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}