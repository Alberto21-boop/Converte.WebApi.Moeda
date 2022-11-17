using Converte.WebApi.Moeda.ServiceBase;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Converte.WebApi.Moeda.Base;

public class BaseDb
{
    internal IDbConnection Db;
    IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    public BaseDb()
    {
        Db = new SqlConnection(Configuration.GetConnectionString("Db"));
    }
}
