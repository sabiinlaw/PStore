using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonStore.Domain.Entities
{
    public class AdminContainer
    {
        public List<Pokemons> Pokemons_for_Admin { get; set; }
        public List<Orders> Orders_for_Admin { get; set; }
       
    }
}
