using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscInventory.Controllers
{
    public class RentalController : Controller
    {
        private discInventoryPKContext context { get; set; }
        public RentalController (discInventoryPKContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var rentals = context.Rentals.
                Include(d => d.Disc).OrderBy(d => d.Disc.DiscName).
                Include(b => b.Borrower).ToList();
            return View(rentals);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Discs = context.Discs.OrderBy(d => d.DiscName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.LName).ToList();
            Rental newrental = new Rental();
            newrental.BorrowDate = DateTime.Today;
            newrental.DueDate = newrental.BorrowDate.AddDays(30);
            return View("Edit", newrental);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Discs = context.Discs.OrderBy(d => d.DiscName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.LName).ToList();
            var rental = context.Rentals.Find(id);
            return View(rental);
        }
        [HttpPost]
        public IActionResult Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (rental.RentalId == 0)
                {
                    context.Rentals.Add(rental);
                }
                else
                {
                    context.Rentals.Update(rental);
                }
                    context.SaveChanges();
                    return RedirectToAction("Index", "Rental");
                
            }
            else
            {
                ViewBag.Action = (rental.RentalId == 0) ? "Add" : "Edit";
                ViewBag.Discs = context.Discs.OrderBy(d => d.DiscName).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.LName).ToList();
                return View(rental);
            }
        }
    }
}
