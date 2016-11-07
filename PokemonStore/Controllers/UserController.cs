using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokemonStore.Domain.Concrete;
using PokemonStore.Domain.Entities;

namespace PokemonStore.Controllers
{
    public class UserController : Controller
    {

        private PokemonStoreDB db = new PokemonStoreDB();
        [Authorize]
        [HttpPost]
        public ActionResult UserProfile()
        {
            string login = HttpContext.User.Identity.Name.ToString();
            Users user = db.Users.FirstOrDefault(u => u.Login.Equals(login));
            if (user.IsAdmin)
            {

                return RedirectToAction("AdminIndex", "Admin");
            }
            return RedirectToAction("Login","Pokemon");
        }

    }
}
