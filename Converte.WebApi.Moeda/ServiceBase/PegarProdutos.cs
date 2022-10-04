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

    public Produtos PegaProdutosPorId(int id)
    {
        return Db.QuerySingleOrDefault<Produtos>("SELECT * FROM produtos WHERE Id = @Id", new { Id = id });
    }

    public void AddProdutos(Produtos produtos)
    {
        string Npgsql = "INSERT INTO Produtos(Id, Produto, ValorProduto, ProdutoMoeda) VALUES(@Id, @Produto, @ValorProduto, @ProdutoMoeda)";

        Db.Query(Npgsql, produtos);

    }

    public void AlteraProduto(Produtos produtos)
    {
        string Npgsql = "UPDATE Produtos SET Id = @Id, Produto = @Produto, ValorProduto = @ValorProduto, NomeMoedaProduto = @NomeMoedaProduto WHERE Id = @Id";

        Db.Execute(Npgsql, produtos);
    }

    public void ApagaProduto(int id)
    {
        Db.Execute("DELETE FROM Produtos WHERE Id = @Id", new { Id = id });
    }

    public double CalculaProdutos(string ProdutoDesejado, string SuaMoeda, string MoedadoProdutoDesejado)
    {


        double moed = Db.QuerySingleOrDefault<double>("SELECT valorproduto FROM Produtos WHERE Produto = @Produto", new { Produto = ProdutoDesejado });

        double moed2 = Db.QuerySingleOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new { NomeMoeda = SuaMoeda });

        double moed3 = Db.QueryFirstOrDefault<double>("SELECT ValorMoeda FROM Moedas WHERE NomeMoeda = @NomeMoeda", new { NomeMoeda = MoedadoProdutoDesejado });

        double resultado = moed * moed2  / moed3;

        return resultado;
    }
}
