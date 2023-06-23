using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NZWalks.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionsDTO> response = new List<RegionsDTO>();


            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7164/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDTO>>());

            }
            catch (Exception)
            {


                throw;
            }


            return View(response);
        }

        [HttpGet]
        public IActionResult AddRegion() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest region)
        {
           
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7164/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(region), Encoding.UTF8, "application/json")
            };

           var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
           
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDTO>();

            if (response != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditRegion(Guid id) 
        {
            var client = httpClientFactory.CreateClient();

           var httpResponseMessage = await client.GetFromJsonAsync<RegionsDTO>($"https://localhost:7164/api/regions/{id.ToString()}");

            if (httpResponseMessage != null) 
            {
                return View(httpResponseMessage);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditRegion(RegionsDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7164/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDTO>();

            if (response != null)
            {
                return RedirectToAction("EditRegion", "Regions");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> DeleteRegion(RegionsDTO request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7164/api/regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index ", "Regions");

            }
            catch (Exception ex)
            {

                return View("EditRegion");
            }
         
           
        }



        [HttpGet]
        public IActionResult Success()
        {
           
            return View();
        }
    }
}
