using Converte.WebApi.Moeda.Model;

namespace Converte.WebApi.Moeda.ServiceBase
{
    public interface InterfaceValor
    {
        public List<Cotacao> PegarTodosValoresDoDia();
        public Cotacao PegaValoresDiaPorId(int idcotacao);
        public void AddValoresDoDia(Cotacao cotacao);
        public void AlteraValoresDoDia(Cotacao cotacao);
        public void ApagaValoresDoDia(int idcotacao);
        public double CalculaMoedas(string Moedadesejada, string SuaMoeda, string cotacao, double valor);
    }
}
