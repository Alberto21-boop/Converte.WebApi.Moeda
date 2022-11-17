using Dapper;

namespace Converte.WebApi.Moeda.Model
{
    [Table("[Vendas]")]
    public class Vendas
    {
        [Key]
        [Column("Id")]
        [IgnoreInsert]
        public int Id { get; set; }
        public string Produto { get; set; } = default!;
        public double Valor { get; set; }
        public int Id_Moedas { get; set; }
        public int Id_Cotacao { get; set; }
        public int Id_Produtos { get; set; }

        public Vendas()
        {

        }
    }
}
