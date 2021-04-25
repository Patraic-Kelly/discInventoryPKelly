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
            var artists = context.Artists.OrderBy(a => a.ArtistLastName).ThenBy(a => a.ArtistName).ToList();
            return View(artists);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            return View("Edit", new Artist());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist rtist)
        {
            if (ModelState.IsValid)
            {
                if (rtist.ArtistId == 0)
                {
                    context.Artists.Add(rtist);
                }
                else
                {
                    context.Artists.Update(rtist);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (rtist.ArtistId == 0);
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
                return View(rtist);
            }
        }
        [HttpGet]

        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            context.Artists.Remove(artist);
            context.SaveChanges();
            return RedirectToAction("Index", "Artist");
        }
    }
}
