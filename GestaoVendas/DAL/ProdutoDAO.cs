using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoVendas.DAL
{
    public class ProdutoDAO
    {
        #region listarProdutos
        public List<ProdutoModel> listarProdutos()
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from produto";

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<ProdutoModel> lista = new List<ProdutoModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProdutoModel item = new ProdutoModel
                    {
                        Idproduto = Convert.ToInt32(dt.Rows[i]["idproduto"].ToString()),
                        Nome = dt.Rows[i]["nome"].ToString(),
                        Descricao = dt.Rows[i]["descricao"].ToString(),
                        Preco_unitario =Convert.ToDecimal(dt.Rows[i]["preco_unitario"].ToString()),
                        Quantidade = Convert.ToInt32(dt.Rows[i]["quantidade"].ToString()),
                        ContentType = dt.Rows[i]["contenttype"].ToString(),
                        Foto = (!Convert.IsDBNull(dt.Rows[i]["foto"]) ? (byte[])dt.Rows[i]["foto"] : null)
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion

        #region Inserir
        public void Inserir(ProdutoModel pro)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"insert into produto(nome, descricao, preco_unitario, 
                                quantidade, foto, contenttype)
                                values (@nome, @descricao, @preco_unitario, 
                                @quantidade, @foto, @contenttype)";

            cmd.Parameters.AddWithValue("nome", pro.Nome);
            cmd.Parameters.AddWithValue("descricao", pro.Descricao);
            cmd.Parameters.AddWithValue("preco_unitario", pro.Preco_unitario);
            cmd.Parameters.AddWithValue("quantidade", pro.Quantidade);
            cmd.Parameters.AddWithValue("foto", pro.Foto);
            cmd.Parameters.AddWithValue("contenttype", pro.ContentType);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion

        #region ConsultarID
        public ProdutoModel ConsultarID(int id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from produto
                            where idproduto=@id";

            cmd.Parameters.AddWithValue("id", id);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            ProdutoModel item = new ProdutoModel();

            if (dt != null)
            {

                item = new ProdutoModel
                {
                    Idproduto = Convert.ToInt32(dt.Rows[0]["idproduto"].ToString()),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Descricao = dt.Rows[0]["descricao"].ToString(),
                    Preco_unitario = Convert.ToDecimal(dt.Rows[0]["preco_unitario"].ToString()),
                    Quantidade = Convert.ToInt32(dt.Rows[0]["quantidade"].ToString()),
                    ContentType = dt.Rows[0]["contenttype"].ToString(),
                    Foto = (!Convert.IsDBNull(dt.Rows[0]["foto"]) ? (byte[])dt.Rows[0]["foto"] : null)
                };
            }
            else
            {
                item = null;
            }

            return item;
        }
        #endregion

        #region Editar
        public void Editar(ProdutoModel pro)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                if (pro.Foto != null)
                {
                    cmd.CommandText = @"update produto
                                set nome = @nome,
                                descricao = @descricao, 
                                preco_unitario = @preco_unitario, 
                                quantidade = @quantidade,
                                foto = @foto,
                                contenttype = @contenttype
                                where idproduto = @id";
                }
                else
                {
                    cmd.CommandText = @"update vendedor
                                set nome = @nome,
                                descricao = @descricao, 
                                preco_unitario = @preco_unitario, 
                                quantidade = @quantidade,                                                                
                                where idvendedor = @id";
                }

                cmd.Parameters.AddWithValue("id", pro.Idproduto);
                cmd.Parameters.AddWithValue("nome", pro.Nome);
                cmd.Parameters.AddWithValue("descricao", pro.Descricao);
                cmd.Parameters.AddWithValue("preco_unitario", pro.Preco_unitario);
                cmd.Parameters.AddWithValue("quantidade", pro.Quantidade);
                cmd.Parameters.AddWithValue("foto", pro.Foto);
                cmd.Parameters.AddWithValue("contenttype", pro.ContentType);


                InstrucaoDAO objDAL = new InstrucaoDAO();
                objDAL.executarSQL(cmd);
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
        }
        #endregion

        #region Excluir
        public void Excluir(int id)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = @"delete from produto
                                where idproduto = @id";

                cmd.Parameters.AddWithValue("id", id);
                InstrucaoDAO objDAL = new InstrucaoDAO();
                objDAL.executarSQL(cmd);
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
        }
        #endregion
    }
}
