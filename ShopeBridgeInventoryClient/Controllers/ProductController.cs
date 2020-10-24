using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ShopeBridgeInventoryClient.Models;

namespace ShopeBridgeInventoryClient.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<Product> products = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44330");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/products").Result;

            if (response.IsSuccessStatusCode)
            {
                products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                ViewBag.result = "No records are available";
            }

            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44330");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/products", product).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(product);
        }

        public ActionResult Details(int id)
        {
            Product product = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44330");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"api/products/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                product = response.Content.ReadAsAsync<Product>().Result;
            }
            else
            {
                ViewBag.result = "No records is available";
            }

            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44330");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"api/products/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                product = response.Content.ReadAsAsync<Product>().Result;
            }
            else
            {
                ViewBag.result = "No records is available";
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Product product)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44330");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync($"api/products/{id}", product).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product product = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44330");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync($"api/products/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }

            return View(product);

        }
    }
}