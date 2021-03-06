﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Itb.Shared;

namespace AspNetCoreMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _prodRepo;

        public ProductController(IProductRepository prodRepo) => _prodRepo = prodRepo;

        // GET: Product
        public async Task<ActionResult> Index()
        {   
            return View(await _prodRepo.GetProducts());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View(_prodRepo.GetProduct(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var prod = new Product() { Name = collection["Name"]};
                _prodRepo.CreateProduct(prod);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var prod = new Product() { Id = id, Name = collection["Name"] };
                _prodRepo.UpdateProduct(prod);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_prodRepo.GetProduct(id));
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _prodRepo.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}