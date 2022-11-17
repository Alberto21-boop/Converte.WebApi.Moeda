using Converte.WebApi.Moeda.Base;
using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarCotacao : BaseDb
{
   

    public List<Cotacao> PegarTodosValoresDoDia()
    {
        return Db.Query<Cotacao>("SELECT * FROM Cotacao").ToList(); ;
    }
    public Cotacao PegaValoresDiaPorId(int id)
    {
        return Db.QuerySingleOrDefault<Cotacao>("SELECT * FROM Moedas WHERE id = @id", new { id = id });
    }

    public void AddValoresDoDia(Cotacao cotacao)
    {
        string Sql = "INSERT INTO Cotacao(Id, [Data], Valor, Id_Moedas) VALUES(@Id, @[Data], @Valordia, @Id_Moedas)";
        Db.Query(Sql, cotacao);
    }

    public void AlteraValoresDoDia(Cotacao cotacao)
    {
        var Sql = @"UPDATE Cotacao SET Valor = @Valor, [Data] = @Data, Id_Moeda = @Id_Moeda WHERE Id = @Id";
        Db.Execute(Sql, cotacao);
    }

    public void ApagaValoresDoDia(int id)
    {
        Db.Execute("DELETE FROM Cotacao WHERE Id = @Id", new { Id = id });
    }
    public double CalculaMoedas(string Moedadesejada, string SuaMoeda, string cotacao, double valor)
    {


        double moed = Db.QueryFirstOrDefault<double>(@"SELECT Cotacao.Valor, Moedas.Id, Cotacao.Id_Moedas, Moedas.Nome, Cotacao.[Data]
                                                     FROM Cotacao
                                                     INNER JOIN Moedas
                                                     ON Moedas.Id = Id_Moedas
                                                     WHERE Moedas.Nome = @Nome AND [Data] = @Data"
                                                     , new { Nome = Moedadesejada, Data = cotacao});

        double moed2 = Db.QueryFirstOrDefault<double>(@"SELECT Cotacao.Valor, Moedas.Id, Cotacao.Id_Moedas, Moedas.Nome, Cotacao.[Data]
                                                     FROM Cotacao
                                                     INNER JOIN Moedas
                                                     ON Moedas.Id = Id_Moedas
                                                     WHERE Moedas.Nome = @Nome AND [Data] = @Data",
                                                     new { Nome = SuaMoeda, Data = cotacao });


        double resultado = valor * (moed2 / moed);

        return resultado;
    }
}
