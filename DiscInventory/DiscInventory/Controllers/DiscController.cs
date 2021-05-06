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
            var discs = context.Discs.OrderBy(d => d.DiscName).
            Include(g => g.Genre).
            Include(s => s.Status).
            Include(m => m.Media).ToList();
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
                    //context.Discs.Add(disc);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disc @p0, @p1, @p2, @p3, @p4", 
                        parameters: 
                        new[] 
                        {
                            disc.DiscName, 
                            disc.ReleaseDate.ToString(), 
                            disc.GenreId.ToString(), 
                            disc.StatusId.ToString(), 
                            disc.MediaId.ToString() 
                        }
                        );
                }
                else
                {
                    //context.Discs.Update(disc);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disc @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] {disc.DiscId.ToString(), disc.DiscName, disc.ReleaseDate.ToString(), disc.GenreId.ToString(), disc.StatusId.ToString(), disc.MediaId.ToString() });
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
            //context.Discs.Remove(disc);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_disc @p0",
               parameters: new[] {disc.DiscId.ToString()});
            return RedirectToAction("Index", "Disc");
        }
    }
}
