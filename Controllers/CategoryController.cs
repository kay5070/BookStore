﻿using Microsoft.AspNetCore.Mvc;
using CuriousMindsBookstore.Models;
using CuriousMindsBookstore.DataAccess;

namespace CuriousMindsBookstore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
        public IActionResult Edit(int? id)
        {
            if (id == null ||  id == 0)
            {
                return NotFound();

            }
            var categoryFrombDb= _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFrombDb == null)
            {
                return NotFound();

            }
            return View(categoryFrombDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFrombDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFrombDb == null)
            {
                return NotFound();

            }
            return View(categoryFrombDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
              _db.Categories.Remove(obj);
              _db.SaveChanges();
              TempData["success"] = "Category Deleted Successfully";
              return RedirectToAction("Index");
          
        }
    }
}
