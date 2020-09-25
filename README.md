[![FVCproductions](https://images5.alphacoders.com/481/thumb-350-481903.png)](https://images5.alphacoders.com/481/thumb-350-481903.png)

# Shakesperean Pokemon

> The REST API for pokemon world in shakespeare's style

**Requirements**

- Visual Studio 2019
- .NET Core 3.1

**Make it running**
In order to run follow the steps below

1. Unzip the ShakespeareanPokemon.zip
2. Open Pokemon.sln with Visual Studio 2019, be sure ASP .NET Core 3.1 is properly installed
3. Press F5 for building and runing the API, nuget will automatically download required packages

You can consume the REST API in the following way, replacing <pokemonname> with the name of your favourite pokemon

> http://localhost:5000/pokemon/<pokemonname>

**API Results**
In case of success the API will return the shakespearean pokemon description in JSON format:

```json
{
  "name": "charizard",
  "description": "Charizard flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself."
}
```

In case the pokemon is not found or an error occurs the followin JSON is returned:

```json
{ "error": { "code": 400, "message": "Unable to retrieve pokemon." } }
```

**External Rsources**
The project uses the following external resource, so make sure your environment can access to Internet:

- https://pokeapi.co/
- https://api.funtranslations.com/translate/

**Known Limitations**
This version is using the free version of https://funtranslations.com/api/shakespeare that supports 60 calls per day, 5 calls per hours. Additional calls results in JSON error message.