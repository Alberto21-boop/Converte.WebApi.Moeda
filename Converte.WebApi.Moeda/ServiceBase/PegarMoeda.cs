using Converte.WebApi.Moeda.Base;
using Converte.WebApi.Moeda.Model;
using Dapper;
using System.Data;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarMoeda : BaseDb
{

    public List<Moedas> PegarTodasMoedas()
    {
        return Db.Query<Moedas>("SELECT * FROM Moedas").ToList();
    }

    public Moedas PegaMoedaPorId(int id)
    {
        return Db.QuerySingleOrDefault<Moedas>("SELECT * FROM Moedas WHERE id = @id", new {id = id});
    }

    public void AddMoedas(Moedas moedas)
    {
        var Sql = "INSERT INTO Moedas(Nome) VALUES(@Nome)";
        Db.Execute(Sql, moedas);
    }

    public void AlteraMoeda(Moedas moedas)
    {
        var Sql = @"UPDATE Moedas SET Nome = @Nome WHERE Id = @Id";
        Db.Execute(Sql, moedas);
    }

    public void ApagaMoeda(int Id)
    {
        Db.Execute("DELETE FROM Moedas WHERE Id = @Id", new {Id = Id});
    }

   
}
