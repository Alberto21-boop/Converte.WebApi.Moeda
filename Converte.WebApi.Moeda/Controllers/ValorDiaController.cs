using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Converte.WebApi.Moeda.Model;
using Converte.WebApi.Moeda.ServiceBase;

namespace Converte.WebApi.Moeda.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValorDiaController : ControllerBase
{
    private PegarCotacao _pegarvalor;

    public ValorDiaController()
    {
        _pegarvalor = new PegarCotacao();
    }

    [HttpGet("PegaTodosValoresDoDia")]
    public IActionResult PegarTodosValoresDoDia()
    {
        return Ok(_pegarvalor.PegarTodosValoresDoDia());
    }

    [HttpGet("PegaValoresDiaPorId/{id}")]
    public IActionResult PegaValoresDiaPorId(int idcotacao)
    {
        var valordia = _pegarvalor.PegaValoresDiaPorId(idcotacao);

        if (valordia == null)
        {
            return NotFound();
        }
        return Ok(valordia);
    }

    [HttpPost("AdicionaValoresDoDia")]
    public IActionResult AddValoresDoDia(Cotacao cotacao)
    {
        _pegarvalor.AddValoresDoDia(cotacao);
        return Ok(cotacao);
    }

    [HttpPut("AlteraOsValoresDoDia")]
    public IActionResult AlteraValoresDoDia(Cotacao cotacao)
    {
        _pegarvalor.AlteraValoresDoDia(cotacao);
        return Ok();
    }

    [HttpDelete("DeletaValorDoDia")]
    public IActionResult ApagaValoresDoDia(int idcotacao)
    {
        _pegarvalor.ApagaValoresDoDia(idcotacao);
        return Ok();
    }
    [HttpPost("CalculaMoedas")]
    public IResult CalculaMoedas(string MoedaDesejada, string SuaMoeda, string cotacao, double valor)
    {
        return Results.Ok(_pegarvalor.CalculaMoedas(MoedaDesejada, SuaMoeda, cotacao, valor));
    }
}
