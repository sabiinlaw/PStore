using PokemonStore.Domain.Abstract;
using PokemonStore.Domain.Entities;
using System.Linq;

namespace PokemonStore.Domain.Concrete
{
   public class EFPokemonRepository:IPokemonRepository
    {
       private PokemonStoreDB context = new PokemonStoreDB();
        public IQueryable<Pokemons> Pokemons
        {
            get { return context.Pokemons; }
        }
    }
}
