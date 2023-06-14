namespace CrudWithGeocoding.Models.Geocoding
{
    public class GeocodingResponse
    {
        public string Status { get; set; }
        public Result[] Results { get; set; }
    }
}
