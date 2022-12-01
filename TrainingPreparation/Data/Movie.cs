using System.Text.Json.Serialization;

namespace TrainingPreparation.Data;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Type { Movie, Series };

public class Movie
{
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("imdbID")]
    public string ImdbId { get; set; }

    [JsonPropertyName("Type")]
    public Type Type { get; set; }

    [JsonPropertyName("Poster")]
    public string Poster { get; set; }
}
