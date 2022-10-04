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
        return (List<Moedas>)Db.Query<Moedas>("SELECT * FROM moedas").ToList();
    }

    public Moedas PegaMoedaPorId(int id)
    {
        return Db.QuerySingleOrDefault<Moedas>("SELECT * FROM moedas WHERE id = @id", new {Id = id});
    }

    public void AddMoedas(Moedas moeda)
    {
        string Npgsql = "INSERT INTO Moedas(Id, nomemoeda, valormoeda) VALUES(@Id, @nomemoeda, @valormoeda)";

        Db.Query(Npgsql, moeda);
    }
    public void AlteraMoeda( Moedas moeda)
    {


        if(moeda == null)
        {
            var Npgsql = Db.Execute(@"UPDATE ""moedas"" SET ""nomemoeda"" = @nomemoeda WHERE ""id"" = @id ", moeda);
            
        }
        else if(moeda == null)
        {
            var Npgsql = Db.Execute(@"UPDATE ""moedas"" SET ""valormoeda"" = @valormoeda WHERE ""id"" = @id ", moeda);
            
        }
        else
        {
            var Npgsql = Db.Execute(@"UPDATE ""moedas"" SET ""nomemoeda"" = @nomemoeda, ""valormoeda"" = @valormoeda 
                                     WHERE ""id"" = @id", moeda);
        }

    }



    public void ApagaMoeda(int id)
    {
        Db.Execute("DELETE FROM moedas WHERE id = @id", new {id = id});
    }

    public double CalculaMoedas(string Moedadesejada, string SuaMoeda, double Valor)
    {
       
            
            double moed = Db.QuerySingleOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new {NomeMoeda = Moedadesejada});
            
            double moed2 = Db.QuerySingleOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new {NomeMoeda = SuaMoeda});
            

            double resultado = moed / moed2 * Valor;

            return resultado;
    }
}
