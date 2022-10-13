namespace Converte.WebApi.Moeda.Model;

public class Produtos
{
    public int Idproduto { get; set; }
    public string Produto { get; set; } = default!;
    public double ValorProduto { get; set; }
    public string ProdutoMoeda{ get; set; }

    public Produtos()
    {

    }
}
