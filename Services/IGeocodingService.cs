using CrudWithGeocoding.Models.Geocoding;
using System;
using System.Threading.Tasks;

namespace CrudWithGeocoding.Services
{
    public interface IGeocodingService
    {
        Task<GeocodingResponse> GetGeocodingResponse(string address, string city);
        Task<Tuple<string, string>> GetGeocodingResponseAsTuple(string address, string city);
    }
}
