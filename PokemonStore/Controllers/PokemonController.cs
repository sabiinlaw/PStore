using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Security.Principal;
using PokemonStore.Domain.Abstract;
using PokemonStore.Domain.Entities;
using PokemonStore.Domain.Concrete;
using PokemonStore.Models;


namespace PokemonStore.Controllers
{
    public class PokemonController : Controller
    {
        private PokemonStoreDB db = new PokemonStoreDB();
        private IPokemonRepository repository;
        private ICrypt crypt;
        public int PageSize = 5;
        public PokemonController(IPokemonRepository pokemonRepository, ICrypt _crypt)
        {
            this.repository = pokemonRepository;
            this.crypt = _crypt;
        }
        public ViewResult List(int page =1)
        {
            PokemonListViewModel model = new PokemonListViewModel
            {
                Pokemons = repository.Pokemons.OrderBy(p=>p.PokemonID)
                .Skip((page-1)*PageSize)
                .Take(PageSize),
               PagingInfo = new PagingInfo{
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = repository.Pokemons.Count()
               }
            };
            return View(model);
        }
        public ActionResult Registration()
        {
            return View("Registration");
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("List", "Pokemon");

        }
        [HttpPost]
        public ActionResult Registration_Check(string login, string password, string name, string email, string phone)
        {
            Random r = new Random();
            int salt = r.Next(0, 100);
            int res = salt / 2;
            string cryptedPassword = password + res.ToString();
            if (db.Users.FirstOrDefault(e => e.Email == email)==null)
            {
                db.Users.Add(new Users
                {
                    Name = name,
                    Email = email,
                    Login = login,
                    CryptedPassword = cryptedPassword,
                    PasswordSalt = salt.ToString(),
                    Phone = phone
                });
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Registration");
            }
        }
        [HttpPost]
        public ActionResult Login_Check(string login, string password)
        {

            int res = this.crypt.GetResult(login, password);
            if (res == 0)
            {
                if (HttpContext.Request.Form.Get("keep") != null)
                    FormsAuthentication.SetAuthCookie(login, true);
                else
                    FormsAuthentication.SetAuthCookie(login, false);
                return RedirectToAction("List");

            }
            return View("Login");
        }
        public ActionResult News()
        {
            var AllOrders = db.Orders.OrderByDescending(d=>d.Date).ToList();
            List<News> news = new List<News>();
            News issue = null;
            int issueID=0;
            foreach (var item in AllOrders)
            {

                if (issue==null||issue.UserID!=item.UserID)
                {
                    if (!news.Exists(i => i.UserID == item.UserID))
                    {
                        issueID++;
                        string name = db.Users.Find(item.UserID).Name;
                        DateTime date = db.Orders.OrderByDescending(d => d.Date).First(u => u.UserID == item.UserID).Date;
                        int count = db.Orders.Where(u => u.UserID == item.UserID).Count();
                        issue = new News
                        {
                            EventID = issueID,
                            UserID = item.UserID,
                            Count = count,
                            UserName = name,
                            Date = date
                        };
                        news.Add(issue);
                    }
                }

            }
           
            return View(news);
        }
        public FileContentResult GetImage(int pokemonID)
        {
            Pokemons pokemon = db.Pokemons.FirstOrDefault(p => p.PokemonID ==pokemonID);
            if (pokemon != null)
            {
                return File(pokemon.ImageData,pokemon.ImageMimeType);

            }
            else return null;
        }
    }
}
