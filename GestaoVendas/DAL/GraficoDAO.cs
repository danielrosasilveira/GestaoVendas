using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoVendas.DAL
{
    public class GraficoDAO
    {
        #region RetornarCARDS
        public GraficoModel RetornarCARDS()
        {
            MySqlCommand cmd = new MySqlCommand();
            InstrucaoDAO objDAL = new InstrucaoDAO();

            DataTable dt;
            GraficoModel item = new GraficoModel();

            string faturamento = "0", cliente = "0", vendedor = "0", produto = "0";

            //CARD FATURAMENTO
            cmd.CommandText = @"select sum(total) as faturamento
                                from venda";

            dt = objDAL.retornoDataTable(cmd);
            
            if (dt != null)
            {
                faturamento = dt.Rows[0]["faturamento"].ToString();
            }

            //CARD CLIENTE
            cmd.CommandText = @"select count(idcliente) as cliente
                                from cliente";
           
            dt = objDAL.retornoDataTable(cmd);
            
            if (dt != null)
            {
                cliente = dt.Rows[0]["cliente"].ToString();
            }

            //CARD Produtos
            cmd.CommandText = @"select count(idproduto) as produto
                                from produto";

            dt = objDAL.retornoDataTable(cmd);

            if (dt != null)
            {
                produto = dt.Rows[0]["produto"].ToString();   
            }

            //CARD Vendedor
            cmd.CommandText = @"select count(idvendedor) as vendedor
                                from vendedor";

            dt = objDAL.retornoDataTable(cmd);

            if (dt != null)
            {
                vendedor = dt.Rows[0]["vendedor"].ToString();
            }


            //MODEL
            if (dt != null)
            {
                item = new GraficoModel
                {
                    Faturamento = Convert.ToDecimal(faturamento),
                    Produto = Convert.ToInt32(produto),
                    Cliente = Convert.ToInt32(cliente),
                    Vendedor = Convert.ToInt32(vendedor)
                };
            }
            return item;
        }
        #endregion

        #region RetornarGRAPH
        public List<GraficoModel> RetornarGRAPH()
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"SELECT sum(total) as valor, 
                                date_format(data, '%d-%m-%y') as data
                                FROM venda
                                where data between (date_sub(CURDATE(), INTERVAL 6 DAY)) 
                                and (date_add(CURDATE(), interval 1 DAY))
                                group by date_format(data, '%d-%m-%y')
                                order by data asc";

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<GraficoModel> lista = new List<GraficoModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GraficoModel item = new GraficoModel
                    {
                        Datadia = dt.Rows[i]["data"].ToString(),
                        Valordia = Convert.ToDecimal(dt.Rows[i]["valor"].ToString())
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion
    }
}
