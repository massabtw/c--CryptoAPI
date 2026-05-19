using CryptoAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CryptoAPI
{
    internal class Database
    {
        private static readonly string Path = System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "data.json");

        public static void Salvar<T>(List<T> lista)
        {
            var opt = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var listaJson = JsonSerializer.Serialize(lista, opt);
            File.WriteAllText(Path, listaJson);

        }

        public static List<T> Ler<T>()
        {
            if (!File.Exists(Path))
            {
                return new List<T>();
            }
            var listaJson = File.ReadAllText(Path);
            var obj = JsonSerializer.Deserialize<List<T>>(listaJson);
            return obj;
        }


    }
}
