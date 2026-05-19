using CryptoAPI;
using CryptoAPI.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/cryptos", () =>
{
    var listaDeMoedas = Database.Ler<HistoricoMoeda>();

    return listaDeMoedas;
}

    );
app.MapGet("/crypto/{moeda}", (string moeda) =>
{
    var listaDeMoedas = Database.Ler<HistoricoMoeda>();
    var moedaEscolhida = listaDeMoedas.LastOrDefault(m => m.Moeda.ToLower() == moeda.ToLower());
    if (moedaEscolhida == null)
    {
        return Results.NotFound("Moeda não encontrada no sistema");
    }
    
        return Results.Ok(moedaEscolhida); 
    
});

app.MapGet("/crypto/atualizar", async () => 
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
        return Results.Problem("Falha ao buscar dados da CoinGecko");
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
    return Results.Ok("Dados carregados!");

});

app.Run();
