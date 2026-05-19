using CryptoAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;

namespace CryptoAPI
{
    internal static class GetApi
    {
        private static readonly HttpClient cliente = new HttpClient();

        static GetApi()
        {
            cliente.Timeout = TimeSpan.FromSeconds(10);
            cliente.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36");
        }
     

        public static async Task<ListaMoedas> CarregarDados(string url)
        {
            try
            {
                var response = await cliente.GetFromJsonAsync<ListaMoedas>(url);
                return response;
            }
            catch(HttpRequestException e)
            {
                if ((int)e.StatusCode == 429)
                {
                    Console.WriteLine("Rate Limit API atingido");
                    await Task.Delay(60000);
                }
            }

            return null;
        }
    }
}