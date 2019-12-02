using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace GestaoVendas.DAL
{
    public class GeralDAO
    {
        #region retornoDataTable
        //Espera um parâmetro do tipo Command
        //contendo um comando SQL do tipo SELECT
        public DataTable retornoDataTable(MySqlCommand cmd)
        {
            try
            {
                DataTable data = new DataTable();
                cmd.Connection = Conexao.Connection;
                Conexao.Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return null;
            }
            finally
            {
                Conexao.Desconectar();
            }
        }
        #endregion

        #region executarSQL
        //Espera um parâmetro do tipo string
        //contendo um comando SQL do tipo INSERT, UPDATE, DELETE
        public void executarSQL(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection = Conexao.Connection;
                Conexao.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            finally
            {
                Conexao.Desconectar();
            }
        }
        #endregion
    }
}
