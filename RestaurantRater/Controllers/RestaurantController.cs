using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // Get: Restaurant/Edit/{id}
        // Get an id from the user
        // Handle if the id is null
        // Find a restaurant by that id
        // If the restaurant doesn't exist
        // Return the restaurant and the view
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = _db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound(); // I could so write: return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            return View(restaurant);
        }

        // Post: Restaurant/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(restaurant).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

    }
}