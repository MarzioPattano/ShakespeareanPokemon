using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Pokemon.DataProxies;
using Pokemon.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    public class PokemonTest: IClassFixture<WebApplicationFactory<Pokemon.Startup>>
    {
        public HttpClient Client { get; }

        public PokemonTest(WebApplicationFactory<Pokemon.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task PokemonProxyIntensiveTest()
        {
            PokemonProxy pokemonProxy = new PokemonProxy();

            string pokemonsUrl = "https://pokeapi.co/api/v2/pokemon/?offset={0}&limit=20";
            int pokemonnumber = 1;
            int count = 0;
            int offset = 0;

            do
            {
                offset += 20;
                PokemonList lst = null;

                string url = string.Format(pokemonsUrl, offset);
                HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                webrequest.Method = "GET";

                using (var response = await webrequest.GetResponseAsync())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    lst = JsonConvert.DeserializeObject<PokemonList>(result);
                }

                if (count == 0)
                {
                    count = lst.count;

                    if (count == 0)
                    {
                        throw new Exception("No pokemons retrieved!!!");
                    }
                }

                // Test
                foreach (var pokemon in lst.results)
                {
                    string pokemonDescription = await pokemonProxy.GetPokemonDescription(pokemon.name);
                    
                    // Just debugging informations
                    System.Diagnostics.Debug.WriteLine($"#{pokemonnumber}");
                    System.Diagnostics.Debug.WriteLine(pokemon.name);
                    System.Diagnostics.Debug.WriteLine(pokemonDescription);

                    pokemonnumber++;

                    Assert.True(!String.IsNullOrWhiteSpace(pokemonDescription));
                }

            } while (offset < count);

        }

        [Fact]
        public async Task ExistingPokemon()
        {
            var response = await Client.GetAsync("/pokemon/charizard");
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            PokemonModel result = JsonConvert.DeserializeObject<PokemonModel>(await response.Content.ReadAsStringAsync());
            Assert.True(result.Description.Length > 1);
        }

        [Fact]
        public async Task NotExistingPokemon()
        {
            var response = await Client.GetAsync("/pokemon/notExistingPokemon");
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}
