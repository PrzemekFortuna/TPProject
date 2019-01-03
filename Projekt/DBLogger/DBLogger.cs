using System.Threading.Tasks;
using MEF;
using Npgsql;

namespace DBLogger
{
    public class DBLogger : ILogger
    {
        private string connectionString = "User ID=npfmrblycmsenb;Password=84e2beaa02230d0c8263938cbcf103aca1d67ea5f3654ce3080f0f277013798a;Host=ec2-54-217-205-90.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9b9j7nr1qnk76;Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;";

        public void Log(LogMode mode, string message)
        {
            Task.Run(() =>
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = string.Format("INSERT INTO LOGS VALUES ({0}, '{1}')", (int)mode, message);
                        command.ExecuteNonQuery();
                    }
                }
            });
        }
    }
}
