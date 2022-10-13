using Converte.WebApi.Moeda.Model;
using Converte.WebApi.Moeda.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Converte.WebApi.Moeda.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private PegarProdutos _pegarprodutos;

    public ProdutosController()
    {
        _pegarprodutos = new PegarProdutos();
    }
    
    [HttpGet("PegaTodosProdutos")]
    public IActionResult PegarTodosOsProdutos()
    {
        return Ok(_pegarprodutos.PegarTodosOsProdutos());
    }

    [HttpGet("PegaProdutoPorId/{id}")]
    public IActionResult PegaProdutosPorId(int id)
    {
        var produtos = _pegarprodutos.PegaProdutosPorId(id);

        if (produtos == null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpPost("AdicionaPropdutos")]
    public IActionResult AddProdutos(Produtos produtos)
    {
        _pegarprodutos.AddProdutos(produtos);
        return Ok(produtos);
    }

    [HttpPut("AlteraProdutos")]
    public IActionResult AlteraProduto(Produtos produtos)
    {
        _pegarprodutos.AlteraProduto(produtos);
        return Ok();
    }

    [HttpDelete("DeletaProduto")]
    public IActionResult ApagaProduto(int id)
    {
        _pegarprodutos.ApagaProduto(id);
        return Ok();
    }
    [HttpPost("CalculaProdutos")]
    public IResult CalculaProdutos(string nomeproduto, string valordamoeda, string data)
    {
        return Results.Ok(_pegarprodutos.CalculaProdutos(nomeproduto, valordamoeda, data));
    }
}
