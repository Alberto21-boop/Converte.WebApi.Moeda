using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase;

public class PegarMoeda : InterfaceMoeda
{

    private static List<Moedas> moedas = new List<Moedas>()
    {
        new Moedas(){Id = 1, NomeMoeda = "BRL", ValorMoeda = 5.00},
        new Moedas(){Id = 2, NomeMoeda = "JPY", ValorMoeda = 136.00},
        new Moedas(){Id = 3, NomeMoeda = "EUR", ValorMoeda = 5.00},
        new Moedas(){Id = 4, NomeMoeda = "GBP", ValorMoeda = 6.00},
        new Moedas(){Id = 5, NomeMoeda = "USD", ValorMoeda = 1.00},
    };

    public List<Moedas> PegarTodasMoedas()
    {
        return moedas;
    }

    public Moedas PegaMoedaPorId(int id)
    {
        return moedas.FirstOrDefault(x => x.Id == id);
    }

    public void AddMoedas(Moedas moeda)
    {
        var UltimaMoeda = moedas.LastOrDefault();

        if(UltimaMoeda == null)
        {
            moeda.Id = 1;
        }
        else
        {
            moeda.Id = UltimaMoeda.Id;
            moeda.Id++;
        }
        moedas.Add(moeda);
    }
    public void AlteraMoeda(Moedas moeda)
    {
        moedas.Remove(moedas.FirstOrDefault(x => x.Id == moeda.Id));
        moedas.Add(moeda);
    }

    public void ApagaMoeda(int id)
    {
        moedas.Remove(moedas.FirstOrDefault(x => x.Id == id));
    }

    public double CalculaMoedas(string MoedaDesejada, string SuaMoeda, double Valor)
    {
        Moedas moed = moedas.FirstOrDefault(x => x.NomeMoeda == (MoedaDesejada));
        
        Moedas moedi = moedas.FirstOrDefault(x => x.NomeMoeda == (SuaMoeda));

        return moed.ValorMoeda / moedi.ValorMoeda * Valor;
    }
}
