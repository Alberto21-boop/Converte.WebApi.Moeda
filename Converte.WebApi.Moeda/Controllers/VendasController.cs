using Converte.WebApi.Moeda.Model;
using Converte.WebApi.Moeda.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Converte.WebApi.Moeda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private PegarVenda _pegarvendas;

        public VendasController()
        {
            _pegarvendas = new PegarVenda();
        }

        [HttpGet("PegaTodasVendas")]
        public IActionResult PegarTodasVendas()
        {
            return Ok(_pegarvendas.PegarTodasVendas());
        }

        [HttpGet("PegaVendasPorId/{id}")]
        public IActionResult PegarVendasPorId(int idvendas)
        {
            var vendas = _pegarvendas.PegarVendasPorId(idvendas);

            if (vendas == null)
            {
                return NotFound();
            }
            return Ok(vendas);
        }

        [HttpPost("AddVendasManualmente")]
        public IActionResult AddVendasManualmente(Vendas vendas)
        {
            _pegarvendas.AddVendasManualmente(vendas);
            return Ok(vendas);
        }

        [HttpPut("AlteraVendas")]
        public IActionResult AlteraVendas(Vendas vendas)
        {
            _pegarvendas.AlteraVendas(vendas);
            return Ok();
        }

        [HttpDelete("DeletaVendas")]
        public IActionResult DeletaVendas(int idvendas)
        {
            _pegarvendas.DeletaVendas(idvendas);
            return Ok();
        }

        [HttpPost("VendaDosProdutos")]
        public IResult VendaDosProdutos(string nomeproduto,string nomemoeda, int moedapagamento, string data,int idvendas, int idproduto, int idcotacao, int idmoedas)
        //public IResult VendaDosProdutos(Vendas vendas)
        {
            return Results.Ok(_pegarvendas.VendaDosProdutos(nomeproduto, nomemoeda,  moedapagamento, data,idvendas, idproduto, idcotacao, idmoedas));
        }
    }
}
