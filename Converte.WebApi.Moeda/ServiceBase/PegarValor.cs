using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarCotacao : InterfaceValor
{
    private IDbConnection Db;

    public PegarCotacao()
    {

        Db = new NpgsqlConnection(@"Server=host.docker.internal;Port=49153;Database=postgres;User Id=postgres;Password=postgrespw;");
    }

    public List<Cotacao> PegarTodosValoresDoDia()
    {
        return (List<Cotacao>)Db.Query<Cotacao>("SELECT * FROM cotacao").ToList();
    }
    public Cotacao PegaValoresDiaPorId(int idcotacao)
    {
        return Db.QuerySingleOrDefault<Cotacao>("SELECT * FROM cotacao WHERE Idcotacao = @idcotacao", new { Idcotacao = idcotacao });
    }

    public void AddValoresDoDia(Cotacao cotacao)
    {
        string Npgsql = "INSERT INTO cotacao(idcotacao, data, moedanome, valordia) VALUES(@idcotacao, @data, @moedanome, @valordia)";

        Db.Query(Npgsql, cotacao);
    }

    public void AlteraValoresDoDia(Cotacao cotacao)
    {
        string Npgsql = "UPDATE Produtos SET Idcotacao = @idcotacao, Data = @data, Moedanome = @Moedanome,  Valordia = @Valordia";

        Db.Execute(Npgsql, cotacao);
    }

    public void ApagaValoresDoDia(int idcotacao)
    {
        Db.Execute("DELETE FROM cotacao WHERE Idcotacao = @idcotacao", new { Idcotacao = idcotacao });
    }
    public double CalculaMoedas(string Moedadesejada, string SuaMoeda, string cotacao, double valor)
    {


        double moed = Db.QueryFirstOrDefault<double>(@"SELECT valordia FROM cotacao AS c 
                                                     INNER JOIN moedas AS m ON m.nomemoeda = c.moedanome 
                                                     WHERE c.moedanome = @moedanome and c.data = @data"
                                                     ,new { moedanome = Moedadesejada, data = cotacao});

        double moed2 = Db.QueryFirstOrDefault<double>(@"SELECT valordia FROM cotacao AS c 
                                                     INNER JOIN moedas AS m ON m.nomemoeda = c.moedanome 
                                                     WHERE c.moedanome = @moedanome and c.data = @data",
                                                     new { moedanome = SuaMoeda, data = cotacao });


        double resultado = valor * (moed2 / moed);

        return resultado;
    }
}
