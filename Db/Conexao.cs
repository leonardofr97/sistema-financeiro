using System;

namespace Db
{
    public class Conexao
    {
        private static readonly string server = "servidor";
        private static readonly string database = "SoN_Financeiro";
        private static readonly string username = "sa";
        private static readonly string password = "dev123";

        public static string GetStringConnection() => $"Server={server};Database={database};User Id={username};Password={password}";
    }
}
