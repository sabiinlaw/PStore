using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonStore.Models
{
    public class News
    {
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}