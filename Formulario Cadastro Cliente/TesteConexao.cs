using System;
using Npgsql;
namespace Formulario_Cadastro_Cliente
{
    public class TesteConexao
    {
        public static void Testar()
        {
            string connString = "Host=localhost;Port=5432;Database=formulario_cliente;Username=postgres;Password=Qzevu**041216";

            using var conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                Console.WriteLine("Conectado com sucesso!");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao conectar : {ex.Message}");
            }
        }
    }
}
