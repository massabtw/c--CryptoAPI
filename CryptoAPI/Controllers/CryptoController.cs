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
            List<string> crypto = [];
            crypto.Add("bitcoin");
            crypto.Add("solana");
            crypto.Add("ethereum");
            crypto.Add("ripple");
            crypto.Add("cardano");

            var ids = string.Join(",", crypto);
            var url = $"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=usd";
            var moedas = await GetApi.CarregarDados(url);

            if (moedas == null)
            {
                return Problem("Falha ao buscar dados da CoinGecko");
            }


            var novaLista = new List<HistoricoMoeda>()
            {
                new() { Moeda = "bitcoin", Valor = moedas.Bitcoin.Usd, TimeStamp = DateTime.Now },
                new() { Moeda = "solana", Valor = moedas.Solana.Usd, TimeStamp = DateTime.Now },
                new() { Moeda = "ethereum", Valor = moedas.Ethereum.Usd, TimeStamp = DateTime.Now },
                new() { Moeda = "ripple", Valor = moedas.Ripple.Usd, TimeStamp = DateTime.Now },
                new() { Moeda = "cardano", Valor = moedas.Cardano.Usd, TimeStamp = DateTime.Now }
            };

            var listData = Database.Ler<HistoricoMoeda>();
            listData.AddRange(novaLista);

            Database.Salvar(listData);
            return Ok("Dados carregados!");
        }
    }
}
