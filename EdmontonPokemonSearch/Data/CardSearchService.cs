using System.Net;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using EdmontonPokemonSearch.Models;

namespace EdmontonPokemonSearch.Data;

public class CardSearchService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CardSearchService> _logger;

    public CardSearchService(IHttpClientFactory httpClientFactory, 
                           ILogger<CardSearchService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<SearchResult>> SearchAllStores(string searchTerm)
    {
        var encodedSearch = WebUtility.UrlEncode(searchTerm);
        var tasks = new List<Task<SearchResult>>();

        tasks.Add(Search203Collectibles(encodedSearch));
        tasks.Add(SearchEclipseGames(encodedSearch));
        tasks.Add(SearchSwirlYEG(encodedSearch));
        tasks.Add(SearchHPWCards(encodedSearch));
        tasks.Add(SearchPokeFam(encodedSearch));

        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    private async Task<SearchResult> Search203Collectibles(string encodedSearch)
    {
        var url = $"https://203collectibles.com/search?q={encodedSearch}";
        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var html = await client.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var products = doc.DocumentNode.SelectNodes("//span[@class='instock']");
            return new SearchResult
            {
                StoreName = "203 Collectibles",
                ItemCount = products?.Count ?? 0,
                StoreUrl = url
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching 203 Collectibles");
            return new SearchResult
            {
                StoreName = "203 Collectibles",
                ItemCount = 0,
                StoreUrl = url,
                ErrorMessage = "Could not retrieve data from this store"
            };
        }
    }

    private async Task<SearchResult> SearchEclipseGames(string encodedSearch)
    {
        var url = $"https://eclipsegames.ca/search?q={encodedSearch}";
        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var html = await client.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var products = doc.DocumentNode.SelectNodes("//span[@class='instock']");
            return new SearchResult
            {
                StoreName = "Eclipse Games",
                ItemCount = products?.Count ?? 0,
                StoreUrl = url
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Eclipse Games");
            return new SearchResult
            {
                StoreName = "Eclipse Games",
                ItemCount = 0,
                StoreUrl = url,
                ErrorMessage = "Could not retrieve data from this store"
            };
        }
    }
    
    private async Task<SearchResult> SearchPokeFam(string encodedSearch)
    {
        var url = $"https://pokefamco.com/search?q={encodedSearch}";
        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var html = await client.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var products = doc.DocumentNode.SelectNodes("//span[@class='instock']");
            return new SearchResult
            {
                StoreName = "PokeFam Co",
                ItemCount = products?.Count ?? 0,
                StoreUrl = url
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Eclipse Games");
            return new SearchResult
            {
                StoreName = "Eclipse Games",
                ItemCount = 0,
                StoreUrl = url,
                ErrorMessage = "Could not retrieve data from this store"
            };
        }
    }

    private async Task<SearchResult> SearchSwirlYEG(string encodedSearch)
    {
        var url = $"https://swirlyeg.com/search?q={encodedSearch}";
        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var html = await client.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var products = doc.DocumentNode.SelectNodes("//li[@data-variantqty]");
            return new SearchResult
            {
                StoreName = "Swirl YEG",
                ItemCount = products?.Count ?? 0,
                StoreUrl = url
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Swirl YEG");
            return new SearchResult
            {
                StoreName = "Swirl YEG",
                ItemCount = 0,
                StoreUrl = url,
                ErrorMessage = "Could not retrieve data from this store"
            };
        }
    }

    private async Task<SearchResult> SearchHPWCards(string encodedSearch)
    {
        var url = $"https://hpwcards.com/search?q={encodedSearch}";
        var client = _httpClientFactory.CreateClient();
        
        try
        {
            var html = await client.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var products = doc.DocumentNode.SelectNodes("//button[contains(text(), 'Add to cart')]");
            return new SearchResult
            {
                StoreName = "HPW Cards",
                ItemCount = products?.Count ?? 0,
                StoreUrl = url
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching HPW Cards");
            return new SearchResult
            {
                StoreName = "HPW Cards",
                ItemCount = 0,
                StoreUrl = url,
                ErrorMessage = "Could not retrieve data from this store"
            };
        }
    }
}