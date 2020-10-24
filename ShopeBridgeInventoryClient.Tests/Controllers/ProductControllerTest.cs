using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopeBridgeInventoryClient;
using ShopeBridgeInventoryClient.Controllers;
using ShopeBridgeInventoryClient.Models;
using System.Web.Mvc;

namespace ShopeBridgeInventoryClient.Tests.Controllers
{
    /// <summary>
    /// Summary description for ProductController
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {
        Product product1 = null;
        Product product2 = null;
        Product product3 = null;
        Product product4 = null;
        Product product5 = null;

        List<Product> products = null;

        ProductController controller = null;
        public ProductControllerTest()
        {
            product1 = new Product { ProductId = 1, ProductName = "Paper", Description = "Paper Description", Price = 2000 };
            product2 = new Product { ProductId = 2, ProductName = "Ink", Description = "Ink Description", Price = 4000 };
            product3 = new Product { ProductId = 3, ProductName = "Pens", Description = "Pen Description", Price = 1000 };
            product4 = new Product { ProductId = 4, ProductName = "Notepads", Description = "Notepad Description", Price = 4500 };
            product5 = new Product { ProductId = 5, ProductName = "Pencil", Description = "Pencil Description", Price = 300 };

            products = new List<Product>
            {
                product1,
                product2,
                product3,
                product4,
                product5
            };
        }

          

        [TestMethod]
        public void Index()
        {
            // Lets call the action method now
            ViewResult result = controller.Index() as ViewResult;

          
            var model = (List<Product>)result.ViewData.Model;

            CollectionAssert.Contains(model, product1);
            CollectionAssert.Contains(model, product2);
            CollectionAssert.Contains(model, product3);
            CollectionAssert.Contains(model, product4);
            CollectionAssert.Contains(model, product5);

        }

        [TestMethod]
        public void Details()
        {
           
            var controller = new ProductController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Create()
        {
         
            Product newproduct = new Product { ProductId = 6, ProductName = "Pins", Description = "new", Price = 433 };
           
            controller.Create(newproduct);
          
            ViewResult result = controller.Index() as ViewResult;

            var model = (List<Product>)result.ViewData.Model;

            CollectionAssert.Contains(model, newproduct);
        }

        [TestMethod]
        public void Edit()
        {            
            Product editproduct = new Product { ProductId = 1, ProductName = "Paper", Description = "A4 Papaer", Price = 3000 };

            controller.Edit(editproduct.ProductId);
            
            ViewResult result = controller.Index() as ViewResult;

            var model = (List<Product>)result.ViewData.Model;

            CollectionAssert.Contains(model, editproduct);
        }

        [TestMethod]
        public void Delete()
        {
            
            controller.Delete(1);           

            ViewResult result = controller.Index() as ViewResult;
        
            var model = (List<Product>)result.ViewData.Model;
            CollectionAssert.DoesNotContain(model, 1);
        }
    }
}
