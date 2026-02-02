using System.Data;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using LibraryManager.Infrastructure.Constants;
using LibraryManager.Infrastructure.Interfaces;

namespace LibraryManager.Infrastructure.Database;
public class DbConnectionFactory : IDbConnectionFactory
{
    string connectionString;
    public DbConnectionFactory(IConfiguration config)
    {
        this.connectionString = config.GetValue<string>(ConnectionStrings.InvoiceGeneratorConnectionString)!;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}

