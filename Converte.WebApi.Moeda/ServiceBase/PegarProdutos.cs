using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarProdutos : InterfaceProdutos
{
    private IDbConnection Db;

    public PegarProdutos()
    {

        Db = new NpgsqlConnection(@"Server=host.docker.internal;Port=49153;Database=postgres;User Id=postgres;Password=postgrespw;");
    }
    
    public List<Produtos> PegarTodosOsProdutos()
    {
        return (List<Produtos>)Db.Query<Produtos>("SELECT * FROM produtos").ToList();
    }

    public Produtos PegaProdutosPorId(int idproduto)
    {
        return Db.QuerySingleOrDefault<Produtos>("SELECT * FROM produtos WHERE Id = @idproduto", new { Id = idproduto });
    }

    public void AddProdutos(Produtos produto)
    {
        string Npgsql = "INSERT INTO Produtos(Idproduto, Produto, ValorProduto, ProdutoMoeda) VALUES(@idproduto, @produto, @valorproduto, @produtomoeda)";

        Db.Query(Npgsql, produto);

    }

    public void AlteraProduto(Produtos produto)
    {
        string Npgsql = "UPDATE Produtos SET Idproduto = @idproduto, Produto = @produto, ValorProduto = @valorproduto, produtomoeda = @produtomoeda WHERE Idproduto = @idproduto";

        Db.Execute(Npgsql, produto);
    }

    public void ApagaProduto(int idproduto)
    {
        Db.Execute("DELETE FROM Produtos WHERE Idproduto = @idproduto", new { Idproduto = idproduto });
    }

    public double CalculaProdutos(string nomeproduto, string valordamoeda, string data)
    {


        double moed2 = Db.QuerySingleOrDefault<double>("SELECT valorproduto FROM produtos WHERE produto = @produto", new { produto = nomeproduto });
        
        
        double moed = Db.QuerySingleOrDefault<double>(@"SELECT valordia FROM cotacao AS c
                                                     LEFT JOIN produtos AS m ON m.produtomoeda = c.moedanome
                                                     WHERE m.produto = @nomeproduto and c.data = @data", 
                                                     new { nomeproduto = nomeproduto, data = data });


        double moed3 = Db.QueryFirstOrDefault<double>(@"SELECT valordia FROM cotacao AS c 
                                                     INNER JOIN moedas AS m ON m.nomemoeda = c.moedanome 
                                                     WHERE c.moedanome = @moedanome and c.data = @data", new { moedanome = valordamoeda, data = data });

        double resultado = moed2 * moed  / moed3;

        return resultado;
    }
}
