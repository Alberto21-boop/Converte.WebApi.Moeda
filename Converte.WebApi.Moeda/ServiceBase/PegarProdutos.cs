using Converte.WebApi.Moeda.Base;
using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarProdutos : BaseDb
{
    
    public List<Produtos> PegarTodosOsProdutos()
    {
        return Db.Query<Produtos>("SELECT * FROM Produtos").ToList();
    }

    public Produtos PegaProdutosPorId(int id)
    {
        return Db.QueryFirstOrDefault<Produtos>("SELECT * FROM Produtos WHERE Id = @Id", new { Id = id });
    }

    public void AddProdutos(Produtos produtos)
    {
        var Sql = "INSERT INTO Produtos(Nome, Valor, Id_Moedas, Id_Cotacao) VALUES(@Nome, @Valor, @Id_Moedas, @Id_Cotacao)";

        Db.Query(Sql, produtos);

    }

    public void AlteraProduto(Produtos produtos)
    {
        var Sql = @"UPDATE Produtos SET  Nome = @Nome, Valor = @Valor, Id_Moedas = @Id_Moedas, Id_Cotacao = @Id_Cotacao WHERE Id = @Id";

        Db.Execute(Sql, produtos);
    }

    public void ApagaProduto(int Id)
    {
        Db.Execute("DELETE FROM Produtos WHERE Id = @Id", new { Id = Id });
    }

    public double CalculaProdutos(string nomeproduto, string valordamoeda, string data)
    {


        double moed2 = Db.QueryFirstOrDefault<double>("SELECT Valor FROM Produtos WHERE Nome = @Nome", 
                                                       new { Nome = nomeproduto });


        double moed = Db.QueryFirstOrDefault<double>(@"SELECT Cotacao.Valor, Produtos.Nome, Cotacao.[Data], Moedas.Nome
                                                     FROM Cotacao
                                                     LEFT JOIN Produtos
                                                     ON Cotacao.Valor = Produtos.Id
                                                     LEFT JOIN Moedas
                                                     ON Moedas.Nome = Produtos.Nome AND [Data] = @Data",
                                                     new { Nome = nomeproduto, Data = data });


        double moed3 = Db.QueryFirstOrDefault<double>(@"SELECT Cotacao.Valor, Moedas.Nome, Cotacao.Id_Moedas, Cotacao.[Data]
                                                      FROM Cotacao
                                                      INNER JOIN Moedas
                                                      ON Moedas.Nome = Moedas.Nome AND [Data] = @Data",
                                                     new { MoedasNome = valordamoeda, Data = data });

        double resultado = moed2 * (moed / moed3);

        return resultado;

    }
        
}
