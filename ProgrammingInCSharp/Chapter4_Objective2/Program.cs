namespace Chapter4_Objective2
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {

        }

        private static async Task SelectDataFromTable()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT * FROM customers", connection);
                await connection.OpenAsync();

                var dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    dataReader.GetString(1);
                }

                dataReader.Close();
            }
        }

        private static void GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            Console.WriteLine($"Opening connection for {connectionString}");
            Thread.Sleep(2000);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                Console.WriteLine("Connection opened");

                Console.WriteLine("Executing transaction...");
                Thread.Sleep(4000);

                Console.WriteLine("Transaction complete");
                Console.WriteLine("Closing connection");
            }
        }
    }
}
