using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase;

public interface InterfaceMoeda
{
    public List<Moedas> PegarTodasMoedas();
    public Moedas PegaMoedaPorId(int idmoeda);
    public void AddMoedas(Moedas moeda);
    public void AlteraMoeda(Moedas moeda);
    public void ApagaMoeda(int idmoeda);
}