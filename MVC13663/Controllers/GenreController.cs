using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC13663.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC13663.Controllers
{
    public class GenreController : Controller
    {
        // GET: GenreController
        private readonly string _baseUrl = "https://localhost:44362/";

        public async Task<ActionResult> Index()
        {
            List<Genre> GenreInfo = new List<Genre>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Genre");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    GenreInfo = JsonConvert.DeserializeObject<List<Genre>>(PrResponse);
                }
            }
            return View(GenreInfo);
        }

        // GET: GenreController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Genre genre = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Genre/{id}");

                // Log response status and content
                Console.WriteLine($"Response Status Code: {Res.StatusCode}");
                var PrResponse = await Res.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {PrResponse}");

                if (Res.IsSuccessStatusCode)
                {
                    genre = JsonConvert.DeserializeObject<Genre>(PrResponse);
                }
            }

            // Checking if the Genre is null after deserialization
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }


        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Genre genre)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(genre);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync("api/Genre", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }


        // GET: GenreController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Genre genre = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Genre/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    genre = JsonConvert.DeserializeObject<Genre>(PrResponse);
                }
            }
            return View(genre);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Genre genre)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(genre);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PutAsync($"api/Genre/{id}", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }

        // GET: GenreController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Genre genre = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Genre/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    genre = JsonConvert.DeserializeObject<Genre>(PrResponse);
                }
            }
            return View(genre);
        }

        // POST: GenreController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync($"api/Genre/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
