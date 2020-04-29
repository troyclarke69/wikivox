using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wikivox.Controllers
{
    [Route("api/[controller]")]
    public class ArtistRestController : Controller
    {
        private readonly IArtist _artist;

        public ArtistRestController(IArtist artist)
        {
            _artist = artist;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _artist.GetAll()
                    .Where(p => p.ArtistName.ToUpper().Contains(term.ToUpper()))
                        .Select(p => p.ArtistName).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Artist> Get()
        {
            return _artist.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Artist Get(int id)
        {
            return _artist.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
