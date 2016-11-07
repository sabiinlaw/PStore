using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PokemonStore.Domain.Entities
{
   public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int PokemonID { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public Pokemons Pokemons { get; set; }
        public Users Users { get; set; }
    }
}
