using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Domain.Abstract
{
   public  interface IOrderProcessor
    {
       void ProcessOrder(OrderContainer container);
    }
}
