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
        public async Task<IActionResult> Delete(int pid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7006/api/Products" + pid))
                {
                    await response.Content.ReadAsStringAsync();
                }
            }
                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit()
        {
            //HttpResponseMessage reponse = await _httpClient.GetAsync(productApiUrl);
            return View();
        }
    }
}
