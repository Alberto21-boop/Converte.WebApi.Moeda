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
        return (List<Moedas>)Db.Query<Moedas>("SELECT * FROM valordia").ToList();
    }

    public Moedas PegaMoedaPorId(int idmoeda)
    {
        return Db.QuerySingleOrDefault<Moedas>("SELECT * FROM valordia WHERE idmoeda = @idmoeda", new {idmoeda = idmoeda});
    }

    public void AddMoedas(Moedas moeda)
    {
        string Npgsql = "INSERT INTO valordia(idvalor, data, moedanome, valordia) VALUES(@idvalor, @data, @moedanome, @valordia)";

        Db.Query(Npgsql, moeda);
    }
    public void AlteraMoeda(Moedas moeda)
    {


        if(moeda == null)
        {
            var Npgsql = Db.Execute(@"UPDATE ""moedas"" SET ""nomemoeda"" = @nomemoeda WHERE ""idmoeda"" = @idmoeda ", moeda);
            
        }
        else
        {
            var Npgsql = Db.Execute(@"UPDATE ""moedas"" SET ""nomemoeda"" = @nomemoeda, WHERE ""idmoeda"" = @idmoeda", moeda);
        }

    }


    public void ApagaMoeda(int idmoeda)
    {
        Db.Execute("DELETE FROM moedas WHERE idmoeda = @idmoeda", new {Idmoeda = idmoeda});
    }

   
}
