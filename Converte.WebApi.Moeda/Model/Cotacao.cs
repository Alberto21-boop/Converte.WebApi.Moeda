namespace Converte.WebApi.Moeda.Model;

public class Cotacao
{
    public int Idcotacao { get; set; }
    public string Data { get; set; }
    public string Moedanome { get; set; } = default!;
    public double Valordia { get; set; }

    public Cotacao()
    {

    }
}
