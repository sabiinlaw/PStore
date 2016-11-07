using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PokemonStore.Domain.Entities
{
   public class Pokemons
    {
       [Key]
       [HiddenInput(DisplayValue = false)]
       public int PokemonID { get; set; }
       [Required(ErrorMessage = "Please enter a pokemon's name")]
       public string Name { get; set; }
       [Required(ErrorMessage = "Please enter a pokemon's description")]
       public string Description { get; set; }
       [DataType(DataType.Currency)]
       [Required(ErrorMessage = "Please enter a pokemon's price")]
       public double Price { get; set; }
       public byte[] ImageData { get; set; }
       [HiddenInput(DisplayValue = false)]
       public string ImageMimeType { get; set; }

   
    }
}
