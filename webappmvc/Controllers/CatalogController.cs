using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webappmvc.Services;
using models;

namespace webappmvc.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;
        public CatalogController(ICatalogService catalogSvc)
        {
            _catalogSvc = catalogSvc;
        }

        public async Task<IActionResult> Index()
        {
            List<product> products = null;
            products = await _catalogSvc.GetProducts();
            return View(products);
        }

        // GET: Simulator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Simulator/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(product prod)
        {
            if (ModelState.IsValid)
            {
                await _catalogSvc.CreateProduct(prod);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(prod);
            }
            // return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            models.product prd = await _catalogSvc.GetProduct(id);
            return View(prd);
        }
    }
}