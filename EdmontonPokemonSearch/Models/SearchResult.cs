namespace EdmontonPokemonSearch.Models;

public class SearchResult
{
    public string StoreName { get; set; } = string.Empty;
    public int ItemCount { get; set; }
    public string StoreUrl { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}