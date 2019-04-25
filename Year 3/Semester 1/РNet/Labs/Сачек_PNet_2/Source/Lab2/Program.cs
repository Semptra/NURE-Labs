using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Lab2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AnimalsDbConnection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    await SelectFromDbAsync(connection, "SELECT * FROM Animals");
                    await InsertIntoDbAsync(connection, "INSERT INTO Animals VALUES(NEWID(), 'MY ANIMAL', 123)");
                    await SelectFromDbAsync(connection, "SELECT * FROM Animals");
                    await ExecuteStoredProcedureAsync(connection, "sp_DeleteAnimal", new Dictionary<string, object> { { "@Name", "MY ANIMAL" } });
                    await SelectFromDbAsync(connection, "SELECT * FROM Animals");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception: {0}", ex.Message);
                }
                finally
                {
                    Console.Write("Press Enter to continue. . . ");
                    Console.ReadLine();
                }
            }
        }

        static async Task SelectFromDbAsync(SqlConnection connection, string selectExpr)
        {
            using (var command = new SqlCommand(selectExpr, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(GetReaderRow(reader));
                }

                Console.WriteLine();
            }
        }

        static async Task InsertIntoDbAsync(SqlConnection connection, string insertExpr)
        {
            using (var command = new SqlCommand(insertExpr, connection))
            {
                int result = await command.ExecuteNonQueryAsync();

                switch (result)
                {
                    case 1:
                        Console.WriteLine("Insert success.");
                        break;
                    default:
                        Console.WriteLine("Error. Result: {0}", result);
                        break;
                }

                Console.WriteLine();
            } 
        }

        static async Task ExecuteStoredProcedureAsync(SqlConnection connection, string procedureName, Dictionary<string, object> procedureParams)
        {
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach(var param in procedureParams)
                {
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));
                }

                var affectedRows = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };

                command.Parameters.Add(affectedRows);

                await command.ExecuteScalarAsync();

                Console.WriteLine("Procedure {0} executed successfully. Affected rows: {1}", procedureName, affectedRows.Value);
                Console.WriteLine();
            }
        }

        static string GetReaderRow(SqlDataReader reader)
        {
            var builder = new StringBuilder();

            for(int i = 0; i < reader.FieldCount; i++)
            {
                builder.Append(reader.GetValue(i).ToString());
                builder.Append("\t");
            }

            return builder.ToString();
        }
    }
}
