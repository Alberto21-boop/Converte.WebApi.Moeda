using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Converte.WebApi.Moeda.Model;
using Converte.WebApi.Moeda.ServiceBase;

namespace Converte.WebApi.Moeda.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private PegarEmails _pegaremail;

    public EmailController()
    {
        _pegaremail = new PegarEmails();
    }

    [HttpGet("PegaTodosEmails")]
    public IActionResult PegarTodosEmails()
    {
        return Ok(_pegaremail.PegarTodosEmails());
    }

    [HttpGet("PegaEmailPorId/{id}")]
    public IActionResult PegaEmailPorId(int id)
    {
        var email = _pegaremail.PegaEmailPorId(id);

        if (email == null)
        {
            return NotFound();
        }
        return Ok(email);
    }

    [HttpPost("AdicionaTemplate")]
    public IActionResult AddTemplate(Email email)
    {
        _pegaremail.AddTemplate(email);
        return Ok(email);
    }

    [HttpPut("AlteraTemplate")]
    public IActionResult AlteraTemplate(Email email)
    {
        _pegaremail.AlteraTemplate(email);
        return Ok();
    }

    [HttpDelete("DeletaTemplate")]
    public IActionResult ApagaEmaiLs(int idemail)
    {
        _pegaremail.ApagaEmaiLs(idemail);
        return Ok();
    }

    [HttpPost("EmailEnviado")]
    public IResult EmailEnviado(string produto, int idemail)
    {
        return Results.Ok(_pegaremail.EmailEnviado(produto, idemail));
    }

}
