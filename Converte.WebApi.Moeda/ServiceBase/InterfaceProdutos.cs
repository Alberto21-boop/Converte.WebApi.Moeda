using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase;

public interface InterfaceProdutos
{
    public List<Produtos> PegarTodosOsProdutos();
    public Produtos PegaProdutosPorId(int id);
    public void AddProdutos(Produtos produtos);
    public void AlteraProduto(Produtos produtos);
    public void ApagaProduto(int id);
    public double CalculaProdutos(string ProdutoDesejado, string SuaMoeda, string ProdutoMoeda);

}
