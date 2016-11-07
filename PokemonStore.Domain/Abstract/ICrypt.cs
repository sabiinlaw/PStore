using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonStore.Domain.Abstract
{
    public interface ICrypt
    {
        int GetResult(string user, string pass);

    }
}
