using PokemonStore.Domain.Abstract;
using PokemonStore.Domain.Entities;
using PokemonStore.Domain.Concrete;
using PokemonStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonStore.Controllers
{
    public class OrderController : Controller
    {
        private IPokemonRepository repository;
        private PokemonStoreDB db = new PokemonStoreDB();
        private IOrderProcessor orderProcessor;
        public OrderController(IPokemonRepository repo, IOrderProcessor proc)
            {
                repository = repo;
                orderProcessor = proc;
            }
        public ActionResult Buy(int pokemonId, string returnUrl, string info=null)
        {
            var container = new OrderContainer();
            var user = db.Users.FirstOrDefault(l => l.Login == HttpContext.User.Identity.Name);
            var pokemon = db.Pokemons.Find(pokemonId);
            if (user != null)
            {
                if (pokemon != null)
                {
                    container.Pokemon_for_Order = pokemon;
                    container.User_for_Order = user;
                    ViewBag.pokemonName = pokemon.Name;
                    if (info != null)
                    {
                        ViewBag.info = info;
                    }
                    return View("OrderForm",container);
                }
                else
                {
                    return RedirectToAction("List", "Pokemon");
                }
        
            }
           
            return  RedirectToAction("Login", "Pokemon");
           
        }
        [HttpPost]
        public ActionResult Checkout(OrderContainer container)
        {
            if (container != null)
            {
                container.order = new Orders();
                container.order.Date = DateTime.Now;
                container.order.Count = 1;
                string result = SaveToOrders(container, out result);
               if (result=="OK")
                {
                   SendLetter(container);
                   return RedirectToAction("News", "Pokemon");
                }
                else
                {
                    if (result != null)
                    {
                        ViewBag.pokemonName = 
                        ViewBag.info = result;
                    }
                    else
                    {
                        ViewBag.info = "There is no user with email: " + container.User_for_Order.Email;
                    }
                   return View("OrderForm");
                }

            }
            return View("List");
        }
        private string SaveToOrders(OrderContainer container, out string result)
        {
            var user = db.Users.FirstOrDefault(e => e.Email == container.User_for_Order.Email);
           
            if (user != null)
            {
                string login = user.Login;
                if (login == HttpContext.User.Identity.Name)
                {
                    Orders newOrder = new Orders
                     {
                         UserID = user.UserID,
                         PokemonID = container.Pokemon_for_Order.PokemonID,
                         Count = container.order.Count,
                         Date = container.order.Date,
                     };

                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    return result = "OK";
                }
                else
                {
                  return result = "This is not your email";
                   
                }
            }
            return result=null;
        }
        private void SendLetter(OrderContainer  container)
        {
            orderProcessor.ProcessOrder(container);
          
        }
    }
}
