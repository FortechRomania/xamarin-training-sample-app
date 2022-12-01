using System.Collections.Generic;
using Newtonsoft.Json;

namespace TrainingPreparation.Data;

public class SearchResponse
{
    [JsonProperty("Search")]
    public List<Movie> Search { get; set; }

    [JsonProperty("totalResults")]
    public string TotalResults { get; set; }

    [JsonProperty("Response")]
    public string Response { get; set; }
}
