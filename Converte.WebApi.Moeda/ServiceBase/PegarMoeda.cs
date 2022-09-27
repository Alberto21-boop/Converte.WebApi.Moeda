using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarMoeda : InterfaceMoeda
{


    private IDbConnection Db;

    public PegarMoeda()
    {
       
        Db = new NpgsqlConnection(@"Server=host.docker.internal;Port=49153;Database=postgres;User Id=postgres;Password=postgrespw;");
    }

    public List<Moedas> PegarTodasMoedas()
    {
        return (List<Moedas>)Db.Query<Moedas>("SELECT * FROM Moedas").ToList();
    }

    public Moedas PegaMoedaPorId(int id)
    {
        return Db.QuerySingleOrDefault<Moedas>("SELECT * FROM Moedas WHERE Id = @Id", new {Id = id});
    }

    public void AddMoedas(Moedas moeda)
    {
        string Npgsql = "INSERT INTO Moedas(Id, NomeMoeda, ValorMoeda) VALUES(@Id, @NomeMoeda, @ValorMoeda)";

        Db.Query(Npgsql, moeda);
    }
    public void AlteraMoeda(Moedas moeda)
    {
        string Npgsql = "UPDATE Moedas SET Id = @Id, NomeMoeda = @NomeMoeda, ValorMoeda = @ValorMoeda WHERE Id = @Id";

        Db.Execute(Npgsql, moeda);
    }

    public void ApagaMoeda(int id)
    {
        Db.Execute("DELETE FROM Moedas WHERE Id = @Id", new {Id = id});
    }

    public double CalculaMoedas(string Moedadesejada, string SuaMoeda, double Valor)
    {
       
            
            double moed = Db.QuerySingleOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new {NomeMoeda = Moedadesejada});
            
            double moed2 = Db.QuerySingleOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new {NomeMoeda = SuaMoeda});
            

            double resultado = moed / moed2 * Valor;

            return resultado;
    }
}
