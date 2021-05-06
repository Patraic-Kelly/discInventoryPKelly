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
            //var borrowers = context.Borrowers.ToList();
            var borrowers = context.Borrowers.OrderBy(l => l.LName).ThenBy(f => f.FName).ToList();
            return View(borrowers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Borrower newborrower = new Borrower();
            return View("Edit", newborrower);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                {
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] 
                        { 
                            borrower.FName,
                            borrower.LName,
                            borrower.Phone.ToString(),
                            borrower.Email
                        });
                }
                else
                {
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3, @p4",
                        parameters: new[]
                        {
                            borrower.BorrowerId.ToString(),
                            borrower.FName,
                            borrower.LName,
                            borrower.Phone.ToString(),
                            borrower.Email.ToString()
                        });
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_borrower @p0",
                parameters: new[]
                {
                    borrower.BorrowerId.ToString(),
                });
            return RedirectToAction("Index", "Borrower");
        }
    }
}
