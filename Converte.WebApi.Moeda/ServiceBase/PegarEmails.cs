using Converte.WebApi.Moeda.Base;
using Converte.WebApi.Moeda.Model;
using Dapper;
using Npgsql;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarEmails : BaseDb
{
    public List<Email> PegarTodosEmails()
    {
        return Db.Query<Email>("SELECT * FROM Email").ToList();
    }

    public Email PegaEmailPorId(int id)
    {
        return Db.QuerySingleOrDefault<Email>("SELECT * FROM Email WHERE Id = @Id", new { id = id });
    }

    public void AddTemplate(Email email)
    {
        string Sql = "INSERT INTO Email(Titulo_Email, Corpo_Email) VALUES(@Titulo_Email, @Corpo_Email)";

        Db.Execute(Sql, email);
    }

    public void AlteraTemplate(Email email)
    {

        var Sql = @"UPDATE Email SET Titulo_Email = @Titulo_Email, Corpo_Email = @Corpo_Email WHERE Id = @Id";
        Db.Execute(Sql, email);

    }

    public void ApagaEmaiLs(int id)
    {
        Db.Execute("DELETE FROM Email WHERE Id = @Id", new { id = id });
    }

    public string EmailEnviado(string produto, int id)
    {
        var emailenviado = Db.QueryFirstOrDefault<string>(@"SELECT Email.Titulo_Email, Email.Corpo_Email, Vendas.Produto
                                                          FROM Email
                                                          LEFT JOIN Vendas
                                                          ON Email.Corpo_Email = Vendas.Produto",
                                                          new { Vendas = produto, id = id });

        return emailenviado;
    }
}
