using GestaoVendas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoVendas.DAL
{
    public class LoginDAO
    {
        #region validarLogin
        public List<VendedorModel> validarLogin(LoginModel login)
        {

            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"select *
                            from vendedor
                            where Email=@email";

            cmd.Parameters.AddWithValue("email", login.Email);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            DataTable dt = objDAL.retornoDataTable(cmd);
            var lista = new List<VendedorModel>();

            foreach (DataRow linha in dt.Rows)
            {
                if (!string.IsNullOrEmpty(linha["idvendedor"].ToString()))
                {
                    var res = new VendedorModel();
                    res.Idvendedor = Convert.ToInt32(dt.Rows[0]["idvendedor"].ToString());
                    res.Nome = dt.Rows[0]["nome"].ToString();
                    res.Sexo = dt.Rows[0]["sexo"].ToString();
                    res.Email = dt.Rows[0]["email"].ToString();
                    res.Senha = dt.Rows[0]["senha"].ToString();
                    res.Nivel = dt.Rows[0]["nivel"].ToString();
                    res.Status = dt.Rows[0]["status"].ToString();
                    res.Foto = (!Convert.IsDBNull(dt.Rows[0]["foto"]) ? (byte[])dt.Rows[0]["foto"] : null);
                    lista.Add(res);
                }
                else
                {
                    lista = null;
                }
            }
            return lista;
        }
        #endregion

        #region alterarSenha
        public void alterarSenha(SenhaModel sen)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = @"update vendedor
                                set senha = @senha
                                where idvendedor = @id";


            cmd.Parameters.AddWithValue("id", sen.Id);
            cmd.Parameters.AddWithValue("senha", sen.NovaSenha);

            InstrucaoDAO objDAL = new InstrucaoDAO();
            objDAL.executarSQL(cmd);
        }
        #endregion
    }
}
