using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase;

public interface InterfaceMoeda
{
    public List<Moedas> PegarTodasMoedas();
    public Moedas PegaMoedaPorId(int id);
    public void AddMoedas(Moedas moeda);
    public void AlteraMoeda( Moedas moeda);
    public void ApagaMoeda(int id);
    public double CalculaMoedas(string MoedaDesejada, string SuaMoeda, double Valor);
}