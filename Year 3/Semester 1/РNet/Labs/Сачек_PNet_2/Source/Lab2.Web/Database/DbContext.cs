using Lab2.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lab2.Web.Database
{
    public class DbContext : IDisposable
    {
        private SqlConnection connection;

        public DbContext(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            Animals = new DbSet<Animal>(connection, nameof(Animals));
        }

        public DbSet<Animal> Animals { get; }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}