using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestaoVendas.DAL
{
    public class VendaDAO
    {
        #region listarVendas
        public List<VendaModel> listarVendas()
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select C.nome as cliente, V.idvenda, V.data, 
                            V.total, Ve.nome as vendedor
                            from cliente C
                            inner join venda V
                            on C.idcliente = V.fk_idcliente
                            inner join vendedor Ve
                            on Ve.idvendedor = V.fk_idvendedor
                            order by idvenda desc";

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

        #region Inserir
        public void Inserir(VendaModel ven)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"insert into venda(data, total, fk_idvendedor, fk_idcliente)
                            values (@data, @total, @fk_idvendedor, @fk_idcliente)";

            cmd.Parameters.AddWithValue("data", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));          
            cmd.Parameters.AddWithValue("total", ven.Total);
            cmd.Parameters.AddWithValue("fk_idvendedor", ven.Vendedor);
            cmd.Parameters.AddWithValue("fk_idcliente", ven.Cliente);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);

            //Recuper o ID da venda
            cmd.CommandText = @"select LAST_INSERT_ID();";
            DataTable dt = objDAL.retornoDataTable(cmd);
            string IDvenda = dt.Rows[0][0].ToString();

            //Deserializar o JSON de produtos
            cmd.CommandText = @"insert into itens_venda(fk_idvenda, fk_idproduto, qtd, valor)
                            values (@venda_id, @produto_id, @qtde_produto, @preco_produto)";
            cmd.Parameters.AddWithValue("venda_id", IDvenda);

            //Comando SQL para baixa do produto
            MySqlCommand baixa = new MySqlCommand();
            baixa.CommandText = @"update produto 
                                set quantidade = quantidade - @qt_produto
                                where idproduto = @prod_id";

            List<Itens_VendaModel> lstProdutos = 
                JsonConvert.DeserializeObject<List<Itens_VendaModel>>(ven.ListaProdutosJSON);
            foreach (var item in lstProdutos)
            {
                //Inserir Itens da Venda
                cmd.Parameters.AddWithValue("produto_id", item.Fk_idproduto);
                cmd.Parameters.AddWithValue("qtde_produto", item.Qtd);
                cmd.Parameters.AddWithValue("preco_produto", Convert.ToDouble(item.Valor));
                objDAL.executarSQL(cmd);

                //Efetivar baixa de produto do estoque 
                baixa.Parameters.AddWithValue("prod_id", item.Fk_idproduto);
                baixa.Parameters.AddWithValue("qt_produto", item.Qtd);
                objDAL.executarSQL(baixa);

                //Limpar Parâmetros
                cmd.Parameters.RemoveAt("produto_id");
                cmd.Parameters.RemoveAt("qtde_produto");
                cmd.Parameters.RemoveAt("preco_produto");
                baixa.Parameters.RemoveAt("prod_id");
                baixa.Parameters.RemoveAt("qt_produto");
            }

        }
        #endregion

        #region Consultar
        public VendaModel ConsultarID(int id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from venda
                            where idvenda=@id";

            cmd.Parameters.AddWithValue("id", id);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            VendaModel item = new VendaModel();

            if (dt != null)
            {
                item = new VendaModel
                {
                    Idvenda = Convert.ToInt32(dt.Rows[0]["idvenda"].ToString()),
                    Data = dt.Rows[0]["data"].ToString(),
                    Total = Convert.ToDecimal(dt.Rows[0]["total"].ToString()),
                    Cliente = dt.Rows[0]["fk_idcliente"].ToString(),
                    Vendedor = dt.Rows[0]["fk_idvendedor"].ToString()
                };
            }
            else
            {
                item = null;
            }
            return item;
        }
        #endregion

        #region Excluir
        public void Excluir(int id)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = @"select *
                            from itens_venda
                            where fk_idvenda = @id";

            cmd.Parameters.AddWithValue("@id", id);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
           
            int codProduto;
            int qtProduto;
           
            MySqlCommand extorno = new MySqlCommand();
            extorno.CommandText = @"update produto 
                                set quantidade = @qt_produto+quantidade
                                where idproduto = @prod_id";

            foreach (DataRow row in dt.Rows)
            {

                qtProduto = Convert.ToInt32(row["qtd"].ToString());
                codProduto = Convert.ToInt32(row["fk_idproduto"].ToString());

                extorno.Parameters.AddWithValue("qt_produto", qtProduto);
                extorno.Parameters.AddWithValue("prod_id", codProduto);
                objDAL.executarSQL(extorno);

                //Limpar Parâmetros
                extorno.Parameters.RemoveAt("@prod_id");
                extorno.Parameters.RemoveAt("@qt_produto");
            }

            //Apaga os Itens da Venda referentes a Venda
            cmd.CommandText = @"delete from itens_venda                                 
                                where fk_idvenda = @fk_idv";
            cmd.Parameters.AddWithValue("fk_idv", id);
            objDAL.executarSQL(cmd);

            //Apaga a Venda
            cmd.CommandText = @"delete from venda                                 
                                where idvenda = @idv";
            cmd.Parameters.AddWithValue("idv", id);
            objDAL.executarSQL(cmd);
        }
        #endregion

    }
}
