using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrainingPreparation.Data;

public class SearchResponse
{
    [JsonPropertyName("Search")]
    public List<Movie> Search { get; set; }

    [JsonPropertyName("totalResults")]
    public string TotalResults { get; set; }

    [JsonPropertyName("Response")]
    public string Response { get; set; }
}
