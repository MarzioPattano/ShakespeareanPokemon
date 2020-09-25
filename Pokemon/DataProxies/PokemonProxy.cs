using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.DataProxies
{
    public class PokemonProxy
    {
        private const string baseUrl = "http://pokeapi.co/api/v2";

        public async Task<string> GetPokemonDescription(string pokemonName)
        {
            PokemonResult pokemon;
            string url = $"{baseUrl}/pokemon/{pokemonName}";
            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = "GET";

            using (var response = await webrequest.GetResponseAsync())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                pokemon = JsonConvert.DeserializeObject<PokemonResult>(result);
            }

            SpeciesResult species;
            string speciesUrl = pokemon.species.url;
            webrequest = (HttpWebRequest)System.Net.WebRequest.Create(speciesUrl);
            webrequest.Method = "GET";

            using (var response = await webrequest.GetResponseAsync())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                species = JsonConvert.DeserializeObject<SpeciesResult>(result);
            }

            string maxDesc = species.flavor_text_entries.Where(x=>x.language.name == "en")
                                                        .Select(x=>x.flavor_text)
                                                        .Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

            return maxDesc;
        }
    }
}
