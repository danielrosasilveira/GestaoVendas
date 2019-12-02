using MySql.Data.MySqlClient;

namespace GestaoVendas.DAL
{
    public class Conexao
    {
        #region Parâmetros de conexão
        private static string Server = "mysql5015.site4now.net";
        private static string Database = "db_a50722_first";
        private static string User = "a50722_first";
        private static string Password = "firstcursos2020";
        private static string connectionString = $@"Server={Server};Database={Database};
                                                    Uid={User};Pwd={Password};SslMode=none;
                                                    Charset=utf8;";
        #endregion

        //Instalar biblioteca MySql.Data -> via Nuget
        public static MySqlConnection Connection = new MySqlConnection(connectionString);

        #region Conectar
        public static void Conectar()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        #endregion

        #region Desconectar
        public static void Desconectar()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {                
                Connection.Close();
            }
        }
        #endregion
    }
}


