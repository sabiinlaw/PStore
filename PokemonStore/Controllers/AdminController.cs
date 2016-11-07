using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using PokemonStore.Domain.Entities;
using PokemonStore.Domain.Concrete;

namespace PokemonStore.Controllers
{
    public class AdminController : Controller
    {
        private PokemonStoreDB db = new PokemonStoreDB();
        [Authorize]
        public ActionResult AdminIndex(string message = null)
        {
            var container = new AdminContainer();
            container.Pokemons_for_Admin = (from Pokemons in db.Pokemons select Pokemons).ToList();
            container.Orders_for_Admin = db.Orders.Include(m => m.Pokemons).Include(f => f.Users).ToList();
            ViewBag.damageMessage = message;
            return View(container);
        }
        [HttpPost]
        public ActionResult AdminLogout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("List", "Pokemon");

        }

        public ActionResult AdminEdit(int pokemonID)
        {
            if (pokemonID >0)
            {

                Pokemons pokemon = db.Pokemons.FirstOrDefault(p => p.PokemonID == pokemonID);
                if (pokemon == null)
                {
                    return HttpNotFound();
                }
                return View("EditPokemon", pokemon);
            }
            return View("AdminIndex");
        }
        public ActionResult AdminDelete(string pokemonID, string orderID)
        {
            string _message = null;
            if (pokemonID!=null)
            {
                int _pokemonID = Convert.ToInt32(pokemonID);
                Pokemons pokemon = db.Pokemons.Find(_pokemonID);
                if (pokemon == null)
                {
                    return HttpNotFound();
                }
              
                var e = from t in db.Orders where t.PokemonID.Equals(_pokemonID) select t;
                if (e.Count() == 0)
                {
                    db.Entry(pokemon).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                {
                    _message = "This product has references in other tables";
                }

                return RedirectToAction("AdminIndex", new { message = _message });

            }
            if (orderID!=null)
            {
                int _orderID = Convert.ToInt32(orderID);
                Orders order = db.Orders.Find(_orderID);
                if (order == null)
                {
                    return HttpNotFound();
                }
                db.Entry(order).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
           
            
            return View("AdminIndex");
        }
      
        [HttpPost]
        public ActionResult PokemonSave(Pokemons pokemon, HttpPostedFileBase image)
        {
            if (pokemon != null)
            {

                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        pokemon.ImageMimeType = image.ContentType;
                        pokemon.ImageData = new byte[image.ContentLength];
                        image.InputStream.Read(pokemon.ImageData, 0, image.ContentLength);
                    }
                    if (pokemon.PokemonID == 0)
                    {
                        db.Entry(pokemon).State = EntityState.Added;
                    }
                    else
                    {
                        db.Entry(pokemon).State = EntityState.Modified;
                        if (image == null)
                        {
                            db.Entry(pokemon).Property(i => i.ImageData).IsModified = false;
                            db.Entry(pokemon).Property(i => i.ImageMimeType).IsModified = false;
                        }

                    }

                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("AdminEdit", new { pokemonID = pokemon.PokemonID });
                }

                return RedirectToAction("AdminIndex");

            }
            return View("EditProduct");
        }
          
        public ActionResult CreatePokemon()
        {
           return View("EditPokemon", new Pokemons());
        }

    }
}
