using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class RelatorioController : Controller
    {
        #region Data - GET
        public IActionResult Data()
        {
            return View();
        }
        #endregion

        #region Data - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Data(RelatorioDataModel rel)
        {
            RelatorioDAO obj = new RelatorioDAO();
            ViewBag.Resultado = obj.ListagemVendasPorData(rel);
            return View();
        }
        #endregion

        #region Vendedor - GET
        public IActionResult Vendedor()
        {
            ViewBag.ListaVendedores = new VendedorDAO().listarVendedores();
            return View();
        }
        #endregion

        #region Vendedor - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VendedorRel(RelatorioVendedorModel rel)
        {
            RelatorioDAO obj = new RelatorioDAO();
            ViewBag.Resultado = obj.ListagemVendasPorVendedor(rel);
            return View();
        }
        #endregion



    }
}