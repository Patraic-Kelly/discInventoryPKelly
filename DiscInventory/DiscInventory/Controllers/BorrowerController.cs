using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscInventory.Models;

namespace DiscInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private discInventoryPKContext context { get; set; }

        public BorrowerController(discInventoryPKContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var borrowers = context.Borrowers.ToList();
            return View(borrowers);
        }
    }
}
