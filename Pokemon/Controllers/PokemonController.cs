using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pokemon.DataProxies;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    [ApiController]
    public class PokemonController : Controller
    {
        private const string baseUrl = "http://pokeapi.co/api/v2";
        private const string errorJson = "{{ \"error\": {{ \"code\": {0}, \"message\": \"{1}\" }}}}";

        [HttpGet("/pokemon/{pokemonname}")]
        public async Task<ActionResult<PokemonModel>> Get(string pokemonname)
        {
            string description = null;
            string descriptionShakespearified = null;
            
            try
            {
                PokemonProxy pokemonProxy = new PokemonProxy();
                description = await pokemonProxy.GetPokemonDescription(pokemonname);
            }
            catch(Exception ex)
            {
                string error = string.Format(errorJson, 400, "Unable to retrieve pokemon.");
                return BadRequest(error);
            }

            try
            {
                ShakespeareProxy shakespeareProxy = new ShakespeareProxy();
                descriptionShakespearified = await shakespeareProxy.Shakespearify(description);
            } catch(Exception )
            {
                string error = string.Format(errorJson, 400, "Shakespeare Generic Error.");
                return BadRequest(error);
            }

            PokemonModel pokemon = new PokemonModel()
            {
                Name = pokemonname,
                Description = descriptionShakespearified
            };

            return pokemon;
        }
    }
}