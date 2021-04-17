using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscInventory.Models;

namespace DiscInventory.Controllers
{
    public class ArtistController : Controller
    {
        private discInventoryPKContext context { get; set; }

        public ArtistController(discInventoryPKContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var artists = context.Artists.ToList();
            return View(artists);
        }
    }
}
