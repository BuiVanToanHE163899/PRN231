using _26_BuiVanToan_BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagementWebClient.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7215/api/products";
            CategoryApiUrl = "https://localhost:7215/api/categories";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl);
            List<Product>? listProducts = new List<Product>();
            if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listProducts = await productResponse.Content.ReadFromJsonAsync<List<Product>>();
            }

            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }

            ViewData["CategoryId"] = new SelectList(listCategories, "CategoryId", "CategoryName");

            return View(listProducts);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
            Product? product = new Product();
            if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = await productResponse.Content.ReadFromJsonAsync<Product>();
            }

            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }
            ViewData["Category"] = listCategories;

            return View(product);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }

            ViewData["Categories"] = listCategories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product p)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, p);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
            Product product = new Product();
            if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = productResponse.Content.ReadFromJsonAsync<Product>().Result;
            }

            List<Category> listCategories = await ApiHandler.DeserializeApiResponse<List<Category>>(CategoryApiUrl, HttpMethod.Get);

            ViewData["Categories"] = listCategories;
        

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductRequest p)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}/{p.ProductId}", p);

                return RedirectToAction("Index");
            }
            // ModelState is invalid, retrieve the categories again
            List<Category> listCategories = await ApiHandler.DeserializeApiResponse<List<Category>>(CategoryApiUrl, HttpMethod.Get);
            ViewData["Categories"] = listCategories;
            return View(p);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await client.DeleteAsync(ProductApiUrl + $"/{id}");
            return RedirectToAction("Index");
        }
    }
}