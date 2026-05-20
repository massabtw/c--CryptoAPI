using CryptoAPI.Modelos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        // GET: api/<CryptoController>
        [HttpGet]
        public IActionResult Get()
        {
            var listaDeMoedas = Database.Ler<HistoricoMoeda>();
            return Ok(listaDeMoedas);
        }

        // GET api/<CryptoController>/5
        [HttpGet("{moeda}")]
        public IActionResult Get(string moeda)
        {
            var listaDeMoedas = Database.Ler<HistoricoMoeda>();
            var moedaEscolhida = listaDeMoedas.LastOrDefault(m => m.Moeda.ToLower() == moeda.ToLower());
            if (moedaEscolhida == null)
            {
                return NotFound("Moeda não encontrada no sistema");
            }

            return Ok(moedaEscolhida);
        }

        // POST api/<CryptoController>
        [HttpPost("atualizar")]
        public async Task<IActionResult> Post()
        {
            await CoinSync.SincronizarMoedas();
            return Ok("Dados carregados!");
        }
    }
}
