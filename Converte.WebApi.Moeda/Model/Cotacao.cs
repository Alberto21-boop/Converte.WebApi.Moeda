using Dapper;

namespace Converte.WebApi.Moeda.Model;

[Table("[Cotacao]")]
public class Cotacao
{
    [Key]
    [Column("Id")]
    [IgnoreInsert]
    public int Id { get; set; }
    public string Data { get; set; }
    public double Valor { get; set; }
    public int Id_Moedas { get; set; }

    public Cotacao()
    {

    }
}
