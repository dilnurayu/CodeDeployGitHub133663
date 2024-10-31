using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC13663.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC13663.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        private readonly string _baseUrl = "https://localhost:44362/";


        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            List<Movie> MovieInfo = new List<Movie>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Movie");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(PrResponse);
                }
            }
            return View(MovieInfo);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Movie movie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Movie/{id}");

                // Log response status and content
                Console.WriteLine($"Response Status Code: {Res.StatusCode}");
                var PrResponse = await Res.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {PrResponse}");

                if (Res.IsSuccessStatusCode)
                {
                    movie = JsonConvert.DeserializeObject<Movie>(PrResponse);
                }
            }

            // Check if the movie is null after deserialization
            if (movie == null)
            {
                return NotFound(); // Return a Not Found view if the movie is null
            }

            return View(movie);
        }


        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(movie);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync("api/Movie", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movie);
        }


        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Movie movie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Movie/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    movie = JsonConvert.DeserializeObject<Movie>(PrResponse);
                }
            }
            return View(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Movie movie)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(movie);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PutAsync($"api/Movie/{id}", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movie);
        }

        // GET: MovieController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Movie movie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Movie/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    movie = JsonConvert.DeserializeObject<Movie>(PrResponse);
                }
            }
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync($"api/Movie/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
