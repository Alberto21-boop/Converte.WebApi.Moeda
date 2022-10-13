using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Converte.WebApi.Moeda.Model;
using Converte.WebApi.Moeda.ServiceBase;

namespace Converte.WebApi.Moeda.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoedaController : ControllerBase
{
    private PegarMoeda _pegarmoeda;

    public MoedaController()
    {
        _pegarmoeda = new PegarMoeda();
    }

    [HttpGet("PegaTodasMoedas")]
    public IActionResult PegarTodasMoedas()
    {
        return Ok(_pegarmoeda.PegarTodasMoedas());
    }

    [HttpGet("PegaMoedaPorId/{id}")]
    public IActionResult PegaMoedaPorId(int id)
    {
        var moedas = _pegarmoeda.PegaMoedaPorId(id);
        
        if(moedas == null)
        {
            return NotFound();
        }
        return Ok(moedas);
    }
    
    [HttpPost("AdicionaMoedas")]
    public IActionResult AddMoedas(Moedas moeda)
    {
        _pegarmoeda.AddMoedas(moeda);
        return Ok(moeda);
    }
    [HttpPut("AlteraMoeda")]
    public IActionResult AlteraMoeda(Moedas moeda)
    {
        _pegarmoeda.AlteraMoeda(moeda);
        return Ok();
    }

    [HttpDelete("DeletaMoeda")]
    public IActionResult ApagaMoeda(int idmoeda)
    {
        _pegarmoeda.ApagaMoeda(idmoeda);
        return Ok();
    }

}
