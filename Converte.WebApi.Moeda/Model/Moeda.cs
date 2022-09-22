namespace Converte.WebApi.Moeda.Model;

public class Moedas
{
    public int Id { get; set; }
    public string NomeMoeda { get; set; } = default!;
    public double ValorMoeda { get; set; }


    public Moedas()
    {
        
    }

}
