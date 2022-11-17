using Dapper;

namespace Converte.WebApi.Moeda.Model;

[Table("[Produtos]")]
public class Produtos 
{
    [Key]
    [Column("Id")]
    [IgnoreInsert]
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public double Valor { get; set; }
    public int Id_Moedas { get; set; }
    public int Id_Cotacao { get; set; }

    public Produtos()
    {

    }
}
