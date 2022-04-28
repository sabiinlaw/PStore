using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonStore.Domain.Entities
{
   public class Pokemons
    {
       [Key]
       public int PokemonID { get; set; }
       [Required(ErrorMessage = "Please enter a pokemon's name")]
       public string Name { get; set; }
       [Required(ErrorMessage = "Please enter a pokemon's description")]
       public string Description { get; set; }
       [DataType(DataType.Currency)]
       [Required(ErrorMessage = "Please enter a pokemon's price")]
       public double Price { get; set; }
       public byte[] ImageData { get; set; }
       public string ImageMimeType { get; set; }
    }
}
