using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PokemonList
    {
        public int count { get; set; }
        public object next { get; set; }
        public string previous { get; set; }
        public List<Result> results { get; set; }
    }


}
