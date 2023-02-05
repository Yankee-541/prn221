using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PrjManageWebClient.Controllers
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
    }
}
