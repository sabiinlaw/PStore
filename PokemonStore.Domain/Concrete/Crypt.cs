using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonStore.Domain.Abstract;
using PokemonStore.Domain.Concrete;

namespace PokemonStore.Domain.Concrete
{
    public class Crypt : ICrypt
    {
        public int GetResult(string login, string password)
        {
            string salt = null;
            string result = null;
            var o = new PokemonStoreDB();
            var user = o.Users.FirstOrDefault(m => m.Login == login);
            o.Dispose();
            if (user != null)
            {
                salt = user.PasswordSalt;
                result = user.CryptedPassword;
                int res = Convert.ToInt32(salt) / 2;
                if (!String.IsNullOrEmpty(login))
                {

                    if (result != null && password.Trim() + res.ToString().Trim() == result.Trim())
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            return -1;
          

        }
    }
}
