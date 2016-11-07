using System.Linq;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Domain.Abstract
{
    public interface IPokemonRepository
    {
        IQueryable<Pokemons> Pokemons { get; }

    }
}
