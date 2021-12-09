using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models.Home;
using System.Linq;
using System;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller    
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> Dishes = _context.Dishes.ToList();

            return View(Dishes);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            return View("NewDish");
        }

        [HttpPost("/create")]
        public IActionResult NewDish(Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                _context.Add(fromForm);
                _context.SaveChanges();
                
                return RedirectToAction("GetOne", new {dishId = fromForm.DishId});
            }
            else
            {
                return View("NewDish");
            }
        }
        
        [HttpGet("/{dishId}")]
        public IActionResult GetOne(int dishId)
        {
            Dish toRender = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

            return View(toRender);
        }
        [HttpGet("/edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish toEdit = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            if(toEdit == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditDish", toEdit);
            }
        }
        [HttpPost("/update/{dishId}")]
        public IActionResult UpdateDish(Dish fromForm, int dishId)
        {
            if(ModelState.IsValid)
            {
                Dish inDatabase = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

                inDatabase.Name = fromForm.Name;
                inDatabase.Chef = fromForm.Chef;
                inDatabase.Tastiness = fromForm.Tastiness;
                inDatabase.Calories = fromForm.Calories;
                inDatabase.Description = fromForm.Description;
                inDatabase.UpdatedAt = DateTime.Now;
                
                _context.SaveChanges();

                return RedirectToAction("GetOne", new {dishId = dishId});
            }
            else
            {
                return EditDish(dishId);
            }
        }
        [HttpGet("/delete/{dishId}")]
        public RedirectToActionResult DeleteDish(int dishId)
        {
            Dish toDelete = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

            _context.Dishes.Remove(toDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}