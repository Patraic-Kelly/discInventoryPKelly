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
            var artists = context.Artists.OrderBy(a => a.ArtistLastName).
            Include(t => t.ArtistType).ToList();
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
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1, @p2",
                        parameters: new[]
                        {
                            artist.ArtistName,
                            artist.ArtistLastName,
                            artist.ArtistTypeId.ToString()
                        });
                }
                else
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2, @p3",
                        parameters: new[] { 
                            artist.ArtistId.ToString(), 
                            artist.ArtistName, 
                            artist.ArtistLastName, 
                            artist.ArtistTypeId.ToString() 
                        });
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
                return View(artist);
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
            //context.Artists.Remove(artist);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0",
            parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
