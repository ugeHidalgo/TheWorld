using Microsoft.Extensions.Logging;
using Microsoft.Framework.Configuration;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private IConfigurationRoot _config;
        private ILogger<GeoCoordsService> _logger;

        public GeoCoordsService(ILogger<GeoCoordsService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoCoordsResult> GetCoordAsync(string name)
        {
            var result = new GeoCoordsResult()
            {
                Success = false,
                Message = "Failed to get coordinates."
            };

            var apiKey = _config["keys:BingKey"];
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            // Read out the results
            // Fragile, might need to change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            return result;
        }
    }
}
