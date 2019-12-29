using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoVendas.Models
{
    public class GraficoModel
    {
        //CARDS
        public decimal Faturamento { get; set; }
        public int Produto { get; set; }
        public int Cliente { get; set; }
        public int Vendedor { get; set; }

        //GRAPH
        public string Datadia { get; set; }
        public decimal Valordia { get; set; }

    }
}
