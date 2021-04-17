using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscInventory.Models;

namespace DiscInventory.Controllers
{
    public class DiscController : Controller
    {
        private discInventoryPKContext context { get; set; }

        public DiscController(discInventoryPKContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var discs = context.Discs.ToList();
            return View(discs);
        }
    }
}
