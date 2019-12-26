using System.Collections.Generic;
using System.IO;
using System.Linq;
using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class ProdutoController : Controller
    {
        #region Index
        public IActionResult Index()
        {
            ProdutoDAO obj = new ProdutoDAO();
            ViewBag.Lista = obj.listarProdutos();
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
        public IActionResult Create(ProdutoModel pro, IList<IFormFile> Image)
        {
            IFormFile uploadedImage = Image.FirstOrDefault();
            if (ModelState.IsValid)
            {
                MemoryStream ms = new MemoryStream();
                if (Image.Count > 0)
                {
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    var size = ms.Length;
                    if (ms.Length > 1048576)
                    {
                        ViewBag.Produto = pro;
                        TempData["error"] = "Limite 1MB";
                        return View();
                    }
                    else
                    {
                        pro.Foto = ms.ToArray();
                        pro.ContentType = uploadedImage.ContentType;
                    }
                }
                ProdutoDAO obj = new ProdutoDAO();
                obj.Inserir(pro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Produto = pro;
            return View();
        }
        #endregion
        
        #region Edit - GET
        public IActionResult Edit(int id)
        {
            ProdutoDAO obj = new ProdutoDAO();
            ViewBag.Produto = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProdutoModel pro, IList<IFormFile> Image)
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
                        ViewBag.Produto = pro;
                        TempData["error"] = "Limite 1MB";
                        return View();
                    }
                    else
                    {
                        pro.Foto = ms.ToArray();
                        pro.ContentType = uploadedImage.ContentType;                 
                    }
                }
                ProdutoDAO obj = new ProdutoDAO();
                obj.Editar(pro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Vendedor = pro;
            return View();
        }
        #endregion

        #region Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProdutoDAO obj = new ProdutoDAO();
            ViewBag.Produto = obj.ConsultarID(id);
            return View();
        }
        #endregion

        #region Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProdutoModel pro)
        {
            ProdutoDAO obj = new ProdutoDAO();
            obj.Excluir(pro.Idproduto);

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}