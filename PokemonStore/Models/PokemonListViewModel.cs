using System.Collections.Generic;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Models
{
    public class PokemonListViewModel
    {
        public IEnumerable<Pokemons> Pokemons { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}