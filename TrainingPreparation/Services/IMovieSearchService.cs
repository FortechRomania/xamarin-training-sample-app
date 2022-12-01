using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrainingPreparation.Data;

namespace TrainingPreparation.Services;

public interface IMovieSearchService
{
    Task<SearchResponse> SearchMovieAsync(string searchTerm);
}

public class MovieSearchService : IMovieSearchService
{
    public async Task<SearchResponse> SearchMovieAsync(string searchTerm)
    {
        using var httpClient = new HttpClient();
        
        var uriBuilder = new UriBuilder("http://www.omdbapi.com");
        uriBuilder.Query = await new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["s"] = searchTerm,
            ["apikey"] = "d8dedd29",
        }).ReadAsStringAsync();

        var response = await httpClient.GetFromJsonAsync<SearchResponse>(uriBuilder.Uri);

        return response;
    }
}
