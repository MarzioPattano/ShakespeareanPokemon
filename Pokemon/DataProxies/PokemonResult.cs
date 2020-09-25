using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.DataProxies
{
    public class Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PokemonResult
    {
        public Species species { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class FlavorTextEntry
    {
        public string flavor_text { get; set; }
        public Language language { get; set; }
    }

    public class SpeciesResult
    {
        public List<FlavorTextEntry> flavor_text_entries { get; set; }
    }
}
