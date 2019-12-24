using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestaoVendas.DAL
{
    public class ClienteDAO
    {

        #region listarClientes
        public List<ClienteModel> listarClientes()
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from cliente";

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            List<ClienteModel> lista = new List<ClienteModel>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClienteModel item = new ClienteModel
                    {
                        Idcliente = Convert.ToInt32(dt.Rows[i]["idcliente"].ToString()),
                        Nome = dt.Rows[i]["nome"].ToString(),
                        Email = dt.Rows[i]["email"].ToString(),
                        Cep = dt.Rows[i]["cep"].ToString(),
                        Logradouro = dt.Rows[i]["logradouro"].ToString(),
                        Numero = dt.Rows[i]["numero"].ToString(),
                        Complemento = dt.Rows[i]["complemento"].ToString(),
                        Bairro = dt.Rows[i]["bairro"].ToString(),
                        Cidade = dt.Rows[i]["cidade"].ToString(),
                        UF = dt.Rows[i]["uf"].ToString(),
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion

        #region Inserir
        public void Inserir(ClienteModel cli)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = @"insert into cliente
                                (nome, cep, logradouro, numero, 
                                complemento, bairro, cidade, uf, email)
                                values 
                                (@nome, @cep, @logradouro, @numero, 
                                @complemento, @bairro, @cidade, @uf, @email)";
            cmd.Parameters.AddWithValue("nome", cli.Nome);
            cmd.Parameters.AddWithValue("cep", cli.Cep);
            cmd.Parameters.AddWithValue("logradouro", cli.Logradouro);
            cmd.Parameters.AddWithValue("numero", cli.Numero);
            cmd.Parameters.AddWithValue("complemento", cli.Complemento);
            cmd.Parameters.AddWithValue("bairro", cli.Bairro);
            cmd.Parameters.AddWithValue("cidade", cli.Cidade);
            cmd.Parameters.AddWithValue("uf", cli.UF);
            cmd.Parameters.AddWithValue("email", cli.Email);
            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion

        #region ConsultarID
        public ClienteModel ConsultarID(int id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from cliente
                            where idcliente=@id";

            cmd.Parameters.AddWithValue("id", id);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            ClienteModel item = new ClienteModel();

            if (dt != null)
            {

                item = new ClienteModel
                {
                    Idcliente = Convert.ToInt32(dt.Rows[0]["idcliente"].ToString()),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Cep = dt.Rows[0]["cep"].ToString(),
                    Logradouro = dt.Rows[0]["logradouro"].ToString(),
                    Numero = dt.Rows[0]["numero"].ToString(),
                    Complemento = dt.Rows[0]["complemento"].ToString(),
                    Bairro = dt.Rows[0]["bairro"].ToString(),
                    Cidade = dt.Rows[0]["cidade"].ToString(),
                    UF = dt.Rows[0]["uf"].ToString(),
                    Email = dt.Rows[0]["email"].ToString()
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
        public void Editar(ClienteModel cli)
        {
            MySqlCommand cmd = new MySqlCommand();



            cmd.CommandText = @"update cliente
                                set nome = @nome,
                                cep = @cep, 
                                logradouro = @logradouro, 
                                numero = @numero,
                                complemento = @complemento,
                                bairro = @bairro,
                                cidade = @cidade,
                                uf = @uf,
                                email = @email
                                where idcliente = @id";

            cmd.Parameters.AddWithValue("id", cli.Idcliente);
            cmd.Parameters.AddWithValue("nome", cli.Nome);
            cmd.Parameters.AddWithValue("cep", cli.Cep);
            cmd.Parameters.AddWithValue("logradouro", cli.Logradouro);
            cmd.Parameters.AddWithValue("numero", cli.Numero);
            cmd.Parameters.AddWithValue("complemento", cli.Complemento);
            cmd.Parameters.AddWithValue("bairro", cli.Bairro);
            cmd.Parameters.AddWithValue("cidade", cli.Cidade);
            cmd.Parameters.AddWithValue("uf", cli.UF);
            cmd.Parameters.AddWithValue("email", cli.Email);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion

        #region Excluir
        public void Excluir(int id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"delete from cliente
                                where idcliente = @id";

            cmd.Parameters.AddWithValue("id", id);
            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion
    }
}
