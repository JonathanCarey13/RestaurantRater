using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _db = new RestaurantDbContext();

        // GET: Restaurant/Index/
        public ActionResult Index()
        {
            return View(_db.Restaurants.ToList());
        }

        // Get: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // Post: Restaurant/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Restaurants.Add(restaurant);    // Add the newly created restaurant to the database
                _db.SaveChanges();                  // Save the changes to the database
                return RedirectToAction("index");   // Then redirect to the index
            }

            return View(restaurant);                // This returns the model that users have already filled out and simply returns it back to the view and populates for us, so you wont lose anything you've filled out
        }

        // Get: Restaurant/Delete/{id}
        public ActionResult Delete(int? id)         // Puting a question mark by something like an int allows it be a nullable value
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = _db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // Post: Restaurant/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Restaurant restaurant = _db.Restaurants.Find(id);
            _db.Restaurants.Remove(restaurant);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}