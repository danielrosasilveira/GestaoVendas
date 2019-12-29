using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.DAL;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            GraficoDAO obj = new GraficoDAO();
            ViewBag.Cards = obj.RetornarCARDS();
            List<GraficoModel> lista = obj.RetornarGRAPH();

            string valores = "";
            string labels = "";
            string numero = "";

            //Percorrer a lista de itens para compor gráfico
            for (int i = 0; i < lista.Count; i++)
            {
                numero = lista[i].Valordia.ToString().Replace(",", ".");
                valores += numero + ",";
                labels += "'" + lista[i].Datadia.ToString() + "',";
                //Escolha aleatória das cores para compor o gráfico
                //cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;

            return View();
        }
    }
}