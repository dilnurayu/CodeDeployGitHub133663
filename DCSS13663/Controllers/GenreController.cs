using DCSS13663.Model;
using DCSS13663.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DCSS13663.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }


        // GET: api/Genre
        [HttpGet]
        public IActionResult Get()
        {
            var g = _genreRepository.GetGenres();
            return new OkObjectResult(g);
        }

        // GET api/Genre/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetByID(int id)
        {
            var g = _genreRepository.GetGenreById(id);
            return new OkObjectResult(g);
        }
        

        // POST api/Genre
        [HttpPost]
        public IActionResult Post([FromBody] Genre g)
        {
            using (var scope = new TransactionScope())
            {
                _genreRepository.InsertGenre(g);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new {id =g.Id}, g);
            }
        }

        // PUT api/Genre/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Genre g)
        {
            if (g != null)
            {
                using (var scope = new TransactionScope())
                {
                    _genreRepository.UpdateGenre(g);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genreRepository.DeleteGenre(id);
            return new OkResult();
        }
    }
}
