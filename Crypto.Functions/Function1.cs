using System;
using CryptoAPI;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Crypto.Functions;

public class Function1
{
    private readonly ILogger _logger;

    public Function1(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Function1>();
    }

    [Function("Function1")]
    public async Task Run([TimerTrigger("*/5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        await CoinSync.SincronizarMoedas();
    }
}