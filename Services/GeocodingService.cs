using CrudWithGeocoding.Models.Geocoding;
using Newtonsoft.Json;

namespace CrudWithGeocoding.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly string apiKey;
        private readonly string apiUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        public GeocodingService(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task<GeocodingResponse> GetGeocodingResponse(string address, string city)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{apiUrl}?address={address}?{city}&key={apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Geocoding API request failed with status code {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var geocodingResponse = JsonConvert.DeserializeObject<GeocodingResponse>(content);

            if (geocodingResponse?.Status != "OK")
            {
                throw new Exception($"Geocoding API request failed with status {geocodingResponse.Status}");
            }

            return geocodingResponse;
        }
        public async Task<Tuple<string, string>> GetGeocodingResponseAsTuple(string address, string city)
        {
            var geocodingResponse = await GetGeocodingResponse(address, city);

            var latitude = geocodingResponse.Results[0].Geometry.Location.Lat.ToString();
            var longitude = geocodingResponse.Results[0].Geometry.Location.Lng.ToString();

            var truncatedLatitude = latitude.Substring(0, Math.Min(latitude.Length, 15));
            var truncatedLongitude = longitude.Substring(0, Math.Min(longitude.Length, 15));

            return Tuple.Create(truncatedLatitude, truncatedLongitude);
        }
    }
    }

