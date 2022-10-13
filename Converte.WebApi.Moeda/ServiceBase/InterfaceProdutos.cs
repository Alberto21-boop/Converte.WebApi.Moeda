using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase;

public interface InterfaceProdutos
{
    public List<Produtos> PegarTodosOsProdutos();
    public Produtos PegaProdutosPorId(int idproduto);
    public void AddProdutos(Produtos produto);
    public void AlteraProduto(Produtos produto);
    public void ApagaProduto(int idproduto);
    public double CalculaProdutos(string nomeproduto, string valordamoeda, string data);

}
