﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;
using Domain;
using GroupProjectWeb.Storage.Contracts;
using GroupProjectWeb.Storage.Implementations;

namespace GroupProjectWeb.Controllers
{
    public class RestaurantsController : Controller
    {
        IStorage storage;


        public RestaurantsController()
        {
            storage = new StorageService();
        }

        // GET: Restaurant
        public async Task<ActionResult> Index()
        {
            var restaurants = await storage.GetAllRestaurants();

            return View(restaurants);
        }

        // GET: Restaurant/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var restaurant = await storage.GetRestaurant(id);
            return View(restaurant);
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public async Task<ActionResult> Create(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", restaurant);
            }

            await storage.AddRestaurant(restaurant);

            return RedirectToAction("Index");
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", restaurant);
            }

            await storage.EditRestaurant(restaurant);
            return RedirectToAction("Index");
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Restaurant restaurant)
        {
            await storage.DeleteRestaurant(restaurant.RestaurantId);
            return RedirectToAction("Index");
        }
    }
}
