using System.Data.Entity;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Domain.Concrete
{
    public class PokemonStoreDB : DbContext
    {
        public PokemonStoreDB()
            : base("name=PokemonStoreDB")
        {
        }
      public DbSet<Pokemons> Pokemons { get; set; }
      public DbSet<Users> Users { get; set; }
      public DbSet<Orders> Orders { get; set; }
    }
}
