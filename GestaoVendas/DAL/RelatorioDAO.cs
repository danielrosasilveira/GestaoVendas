using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoVendas.DAL
{
    public class RelatorioDAO
    {
        #region ListagemVendasporData
        public List<VendaModel> ListagemVendasPorData(RelatorioDataModel rel)
        {
            MySqlCommand cmd = new MySqlCommand();

            if (rel.DataInicio.Year != 1)
            {

                cmd.CommandText = @"select V1.idvenda, V1.data, V1.total, 
                                    V2.nome as vendedor, C.nome as cliente
                                    from cliente C 
                                    inner join venda V1
                                    on C.idcliente = V1.fk_idcliente
                                    inner join vendedor V2
                                    on V2.idvendedor = V1.fk_idvendedor                             
                                    where v1.data between @DataInicio and @DataFim
                                    order by V1.data, V1.total";
            }
            else
            {
                cmd.CommandText = @"select V1.idvenda, V1.data, V1.total, 
                                    V2.nome as vendedor, C.nome as cliente
                                    from cliente C 
                                    inner join venda V1
                                    on C.idcliente = V1.fk_idcliente
                                    inner join vendedor V2
                                    on V2.idvendedor = V1.fk_idvendedor
                                    order by V1.data, V1.total";
            }

            cmd.Parameters.AddWithValue("@DataInicio", rel.DataInicio.ToString("yyyy/MM/dd 00:00:00"));
            cmd.Parameters.AddWithValue("@DataFim", rel.DataFim.ToString("yyyy/MM/dd 23:59:59"));

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<VendaModel> lista = new List<VendaModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VendaModel item = new VendaModel
                    {
                        Idvenda = Convert.ToInt32(dt.Rows[i]["idvenda"].ToString()),
                        Data = Convert.ToDateTime(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                        Total = Convert.ToDecimal(dt.Rows[i]["total"].ToString()),
                        Cliente = dt.Rows[i]["cliente"].ToString(),
                        Vendedor = dt.Rows[i]["vendedor"].ToString()
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion

        #region ListagemVendasporVendedor
        public List<VendaModel> ListagemVendasPorVendedor(RelatorioVendedorModel rel)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select V1.idvenda, v1.data, v1.total, 
                                V2.nome as vendedor, C.nome as cliente
                                from cliente C                                
                                inner join venda V1 
                                on C.idcliente = V1.fk_idcliente
                                inner join vendedor V2
                                on V2.idvendedor = V1.fk_idvendedor
                                where V1.fk_idvendedor = @fk_idvendedor";

            cmd.Parameters.AddWithValue("@fk_idvendedor", rel.Vendedor);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<VendaModel> lista = new List<VendaModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VendaModel item = new VendaModel
                    {
                        Idvenda = Convert.ToInt32(dt.Rows[i]["idvenda"].ToString()),
                        Data = Convert.ToDateTime(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                        Total = Convert.ToDecimal(dt.Rows[i]["total"].ToString()),
                        Cliente = dt.Rows[i]["cliente"].ToString(),
                        Vendedor = dt.Rows[i]["vendedor"].ToString()
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion
    }
}
