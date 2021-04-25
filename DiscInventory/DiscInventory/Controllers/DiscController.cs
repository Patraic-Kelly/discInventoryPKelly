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
            var discs = context.Discs.OrderBy(d => d.DiscName).ThenBy(s => s.StatusId).ToList();
            return View(discs);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
            ViewBag.DiscStatuses = context.DiscStatuses.OrderBy(s => s.StatusDesc).ToList();
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(m => m.MediaDesc).ToList();
            Disc newdisc = new Disc();
            newdisc.ReleaseDate = DateTime.Today;
            return View("Edit", newdisc);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
            ViewBag.DiscStatuses = context.DiscStatuses.OrderBy(s => s.StatusDesc).ToList();
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(m => m.MediaDesc).ToList();
            var disc = context.Discs.Find(id);
            return View(disc);
        }

        [HttpPost]
        public IActionResult Edit(Disc disc)
        {
            if (ModelState.IsValid)
            {
                if (disc.DiscId == 0)
                {
                    context.Discs.Add(disc);
                }
                else
                {
                    context.Discs.Update(disc);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Disc");
            }
            else
            {
                ViewBag.Action = (disc.DiscId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
                ViewBag.DiscStatuses = context.DiscStatuses.OrderBy(s => s.StatusDesc).ToList();
                ViewBag.MediaTypes = context.MediaTypes.OrderBy(m => m.MediaDesc).ToList();
                return View(disc);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disc = context.Discs.Find(id);
            return View(disc);
        }

        [HttpPost]
        public IActionResult Delete(Disc disc)
        {
            context.Discs.Remove(disc);
            context.SaveChanges();
            return RedirectToAction("Index", "Disc");
        }
    }
}
