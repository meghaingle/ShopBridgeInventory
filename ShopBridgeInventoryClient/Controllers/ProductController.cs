using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBridgeInventoryClient.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace ShopBridgeInventoryClient.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public static HttpClient webClient = new HttpClient();
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
                ViewBag.result = "Error";
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
    }
}