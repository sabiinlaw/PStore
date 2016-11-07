using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Domain.Entities
{
    public class OrderContainer
    {
        public Pokemons Pokemon_for_Order { get; set; }
        public Users User_for_Order { get; set; }
        public Orders order { get; set; }
    }
}