using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4_150348;
using LAB4_150348.Models;
using LAB4_150348.ViewModels;

namespace LAB4_150348.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.Books = _context.Books;

            if (id.HasValue)
            {
                var order = await _context.Orders
                    .Include(o => o.BookOrders)
                    .ThenInclude(bookOrder => bookOrder.Book)
                    .FirstOrDefaultAsync(o => o.Id == id);
                return View(order);
            }

            var entity = new Order();
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return View(entity);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] int id)
        {
            if (ModelState.IsValid)
            {
                var order = await _context.Orders
                    .Include(o => o.BookOrders)
                    .ThenInclude(bookOrder => bookOrder.Book)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return NotFound();
                }

                order.FinishOrder();

                _context.Update(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,TotalPrice")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddBookToOrder([Bind("BookId,BookAmount,OrderId")] BookOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await _context.Orders.FindAsync(model.OrderId);
                var book = await _context.Books.FindAsync(model.BookId);

                if (book == null) return BadRequest($"Book with id {model.BookId} does not exists.");
                if (order == null) return BadRequest($"Order with id {model.OrderId} does not exists.");


                order.BookOrders.Add(new BookOrder
                {
                    Book = book,
                    Order = order,
                    BookAmount = model.BookAmount
                });

                order.TotalPrice += book.Price * model.BookAmount;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Create", new { id = model.OrderId });
        }
    }
}
