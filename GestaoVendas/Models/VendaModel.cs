using System;

namespace GestaoVendas.Models
{
    public class VendaModel
    {
        public int Idvenda { get; set; }
        public string Data { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public decimal Total { get; set; }

        public string ListaProdutosJSON { get; set; }

    }
}


