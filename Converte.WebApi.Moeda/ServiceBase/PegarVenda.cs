using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;
using System.Data;
using Converte.WebApi.Moeda.Base;

namespace Converte.WebApi.Moeda.ServiceBase
{
    public class PegarVenda : BaseDb
    {
        

        public List<Vendas> PegarTodasVendas()
        {
            return Db.Query<Vendas>("SELECT * FROM Vendas").ToList();
        }
        public Vendas PegarVendasPorId(int id)
        {
            return Db.QuerySingleOrDefault<Vendas>("SELECT * FROM Vendas WHERE Id = @id", new { id = id });
        }

        public void AddVendasManualmente(Vendas vendas)
        {
            string Sql = "INSERT INTO Vendas(Id, Produto, Valor, Id_Moedas, Id_Cotacao, Id_Produtos) VALUES(@Id, @Produto, @Valor, @Id_Moedas, @Id_Cotacao, @Id_Produtos)";

            Db.Query(Sql, vendas);
        }

        public void AlteraVendas(Vendas vendas)
        {
            var Sql = "UPDATE Vendas SET Produto = @Produto, Valor = @Valor, Id_Moedas = @Id_Moedas, Id_Cotacao = @Id_Cotacao, Id_Produtos = @Id_Produtos WHERE Id = @Id";
            Db.Execute(Sql, vendas);
        }

        public void DeletaVendas(int id)
        {
            Db.Execute("DELETE FROM Vendas WHERE Id = @Id", new { id = id });
        }

        public double VendaDosProdutos(string nomeproduto,string nomemoeda, int moedapagamento, string data,int idvendas, int idprodutos, int idcotacao, int idmoedas)
        {


            double moedvenda = Db.QueryFirstOrDefault<double>(@"SELECT (p.Valor * (c2.Valor / c.Valor))
                                                              FROM Produtos AS p  
                                                              LEFT JOIN Cotacao AS c ON c.Id_Moedas = p.Id_Moedas
                                                              LEFT JOIN Cotacao AS c2 ON c2.Id_Moedas = @Id_Moedas
                                                              WHERE c.[Data] = @Data AND c2.[Data] = @Data 
                                                              AND p.Nome = @Nome",
                                                             new { Nome = nomeproduto, Id_Moedas = moedapagamento, Data = data });

            string command = "INSERT INTO Vendas(Produto, Valor, Id_Moedas, Id_Cotacao, Id_Produtos) " +
                              "VALUES(@Produto, @Valor, @Id_Moedas, @Id_Cotacao, @Id_Produtos)";

            var sqlInsert = Db.Execute(command, new {  Produto = nomeproduto, Id_Produtos = idprodutos, Id_Cotacao = idcotacao, Id_Moedas = idmoedas, Valor = moedvenda});

            return moedvenda;

        }
    }
}
