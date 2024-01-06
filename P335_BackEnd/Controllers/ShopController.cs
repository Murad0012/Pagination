using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Models;

namespace P335_BackEnd.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page, string order)
        {
            if (page <= 0) page = 1;

            int productsPerPage = 4;
            var productCount = _dbContext.Products.Count();

            ViewBag.Sales = _dbContext.Products.Include(x => x.Sale).ToList();

            var products = _dbContext.Products.AsQueryable();
            ViewData["IdOrder"] = string.IsNullOrEmpty(order) ? "id_desc" : "";

            switch (order)
            {
                case "id_desc":
                    products = products.OrderByDescending(x => x.Id);
                    ViewBag.Order = "id_desc";
                    break;
                default:
                    products = products.OrderBy(x => x.Id);
                    ViewBag.Order = ""; 
                    break;
            }

            ViewBag.Order = order;

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var model = new ShopIndexVM
            {
                Products = products
                    .Skip((page - 1) * productsPerPage)
                    .Take(productsPerPage)
                    .ToList(),
                TotalPageCount = totalPageCount,
                CurrentPage = page
            };

            
            return View(model);
        }
    }
}