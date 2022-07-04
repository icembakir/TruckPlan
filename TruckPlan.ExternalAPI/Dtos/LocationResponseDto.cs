using Newtonsoft.Json;

namespace TruckPlan.ExternalAPI.Dtos
{       

    public partial class LocationResponseDto
    {
        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public object Number { get; set; }

        [JsonProperty("postal_code")]
        public object PostalCode { get; set; }

        [JsonProperty("street")]
        public object Street { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("locality")]
        public object Locality { get; set; }

        [JsonProperty("administrative_area")]
        public object AdministrativeArea { get; set; }

        [JsonProperty("neighbourhood")]
        public object Neighbourhood { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}

