using System;
using Newtonsoft.Json;

namespace TrainingPreparation.Data
{
    public enum Type { Movie, Series };

    public class Movie
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }

        [JsonProperty("Type")]
        public Type Type { get; set; }

        [JsonProperty("Poster")]
        public string Poster { get; set; }
    }
}
