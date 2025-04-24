using System.ComponentModel.DataAnnotations;

namespace EdmontonPokemonSearch.Models;

public class SearchModel
{
    [Required(ErrorMessage = "Card name is required")]
    [StringLength(50, ErrorMessage = "Card name is too long")]
    public string CardName { get; set; } = string.Empty;
    
    [StringLength(20, ErrorMessage = "Collection number is too long")]
    public string CollectionNumber { get; set; } = string.Empty;
    
    [StringLength(50, ErrorMessage = "Set name is too long")]
    public string SetName { get; set; } = string.Empty;
}