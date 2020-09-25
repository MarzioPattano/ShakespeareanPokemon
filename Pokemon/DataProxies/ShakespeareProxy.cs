using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.DataProxies
{
    public class ShakespeareProxy
    {
        private const string baseUrl = "https://api.funtranslations.com/translate/shakespeare.json";

        public async Task<string> Shakespearify(string text)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(baseUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                string json = $"{{\"text\":\"{text.Replace("\n", " ")}\"}}";

                streamWriter.Write(json);
            }

            var httpResponse = await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var shakespeareResult = JsonConvert.DeserializeObject<ShakespeareResult>(result);

                return shakespeareResult.contents.text;
            }
        }
    }
}
