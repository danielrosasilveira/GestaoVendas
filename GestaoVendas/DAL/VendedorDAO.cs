using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestaoVendas.DAL
{
    public class VendedorDAO
    {
        #region listarVendedores
        public List<VendedorModel> listarVendedores()
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from vendedor";

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<VendedorModel> lista = new List<VendedorModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VendedorModel item = new VendedorModel
                    {
                        Idvendedor = Convert.ToInt32(dt.Rows[i]["idvendedor"].ToString()),
                        Nome = dt.Rows[i]["nome"].ToString(),
                        Sexo = dt.Rows[i]["sexo"].ToString(),
                        Email = dt.Rows[i]["email"].ToString(),
                        Nivel = dt.Rows[i]["nivel"].ToString(),
                        Status = dt.Rows[i]["status"].ToString(),
                        Foto = (!Convert.IsDBNull(dt.Rows[i]["foto"]) ? (byte[])dt.Rows[i]["foto"] : null)

                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion

        #region Inserir
        public void Inserir(VendedorModel ven)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"insert into vendedor(nome, sexo, email, senha, nivel, status, foto)
                            values (@nome, @sexo, @email, @senha, @nivel, @status, @foto)";

            cmd.Parameters.AddWithValue("nome", ven.Nome);
            cmd.Parameters.AddWithValue("sexo", ven.Sexo);
            cmd.Parameters.AddWithValue("email", ven.Email);
            cmd.Parameters.AddWithValue("senha", ven.Senha);
            cmd.Parameters.AddWithValue("nivel", ven.Nivel);
            cmd.Parameters.AddWithValue("status", ven.Status);
            cmd.Parameters.AddWithValue("foto", ven.Foto);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion
 
        #region ConsultarID
        public VendedorModel ConsultarID(int id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from vendedor
                            where idvendedor=@id";

            cmd.Parameters.AddWithValue("id", id);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            VendedorModel item = new VendedorModel();

            if (dt != null)
            {

                item = new VendedorModel
                {
                    Idvendedor = Convert.ToInt32(dt.Rows[0]["idvendedor"].ToString()),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Sexo = dt.Rows[0]["sexo"].ToString(),
                    Email = dt.Rows[0]["email"].ToString(),
                    Senha = dt.Rows[0]["senha"].ToString(),
                    Nivel = dt.Rows[0]["nivel"].ToString(),
                    Status = dt.Rows[0]["status"].ToString(),
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
        public void Editar(VendedorModel ven)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                if (ven.Foto != null)
                {
                    cmd.CommandText = @"update vendedor
                                set nome = @nome,
                                sexo = @sexo, 
                                email = @email, 
                                nivel = @nivel,
                                status = @status,
                                foto = @foto,
                                contenttype = @contenttype
                                where idvendedor = @id";
                }
                else
                {
                    cmd.CommandText = @"update vendedor
                                set nome = @nome,
                                sexo = @sexo, 
                                email = @email, 
                                nivel = @nivel,
                                status = @status
                                where idvendedor = @id";
                }

                cmd.Parameters.AddWithValue("id", ven.Idvendedor);
                cmd.Parameters.AddWithValue("nome", ven.Nome);
                cmd.Parameters.AddWithValue("sexo", ven.Sexo);
                cmd.Parameters.AddWithValue("email", ven.Email);
                cmd.Parameters.AddWithValue("nivel", ven.Nivel);
                cmd.Parameters.AddWithValue("status", ven.Status);
                cmd.Parameters.AddWithValue("foto", ven.Foto);
                cmd.Parameters.AddWithValue("contenttype", ven.ContentType);

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

                cmd.CommandText = @"delete from vendedor
                                where idvendedor = @id";

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


