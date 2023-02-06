using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private string productApiUrl = "";
        public ProductController()
        {
            _httpClient = new HttpClient();
            var contenttype = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contenttype);
            productApiUrl = "https://localhost:7006/api/Products";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage reponse = await _httpClient.GetAsync(productApiUrl);
            string strData = await reponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);

            return View(listProducts);
        }

        public async Task<IActionResult> Create([FromForm] Product product)
        {
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(productApiUrl, product))
                {
                    await response.Content.ReadAsStringAsync();
                } 
            }


            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? pid)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.DeleteAsync("https://localhost:7006/api/Products" + pid))
            //    {
            //        await response.Content.ReadAsStringAsync();
            //    }
            //}
            //----
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _client.DeleteAsync("https://localhost:7006/api/Products/{pid}");
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                TempData["msg"] = "Delete Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                TempData["msg"] = "Delete Failed. Try again!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int? id)
        {
            var product = await GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await GetCategories();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] ProductDto model)
        {
            ViewBag.Categories = await GetCategories();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HttpResponseMessage response = await _client.PutAsJsonAsync("https://localhost:7006/api/Products/Update", model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                ViewData["msg"] = "Update Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                ViewData["msg"] = "Update Failed. Try again!";
            }

            return View(model);
        }
    }
}
