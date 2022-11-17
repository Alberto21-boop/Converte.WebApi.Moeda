using Dapper;


namespace Converte.WebApi.Moeda.Model;

[Table("[Moedas]")]
public class Moedas
{
    [Key]
    [Column("Id")]
    [IgnoreInsert]
    public int Id { get; set; }
    public string Nome { get; set; } = default!;

    public Moedas()
    {
        
    }


}
