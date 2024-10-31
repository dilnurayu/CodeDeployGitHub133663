using DCSS13663.Model;
using DCSS13663.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DCSS13663.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        // GET: api/Movie
        [HttpGet]
        public IActionResult Get()
        {
            var movies = _movieRepository.GetMovies();
            return new OkObjectResult(movies);
            //return new string[] { "value1", "value2" };
        }


        // GET: api/Movie/5
        [HttpGet("{id}", Name = "GetMovies")]
        public IActionResult Get(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return new OkObjectResult(movie);
            //return "value";
        }


        // POST api/Movie
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            using (var scope = new TransactionScope())
            {
                _movieRepository.InsertMovie(movie);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            }
        }

        // PUT api/Movie/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            if (movie != null)
            {
                using (var scope = new TransactionScope())
                {
                    _movieRepository.UpdateMovie(movie);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/Movie/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _movieRepository.DeleteMovie(id);
            return new OkResult();
        }

    }
}
