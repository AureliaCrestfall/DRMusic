using DRMusic.Model;
using DRMusic.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DRMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private MusicRepo _musicRepo;

        public MusicController(MusicRepo musicrepo)
        {
            _musicRepo = musicrepo;
        }

        // GET: api/<MusicController>
        [HttpGet]
        public ActionResult<IEnumerable<Music>>Get()
        {
             
            List<Music> music = _musicRepo.GetAllMusics();
            if (music != null)
            {


                return Ok(music);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET api/<MusicController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Music> Get(int id)
        {
            
            Music music = _musicRepo.Get(id);
            if (music != null)
            {


                return Ok(music);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<MusicController>
        [HttpPost]
        public ActionResult<Music> Post([FromBody] Music music)
        {
            Music created = _musicRepo.Add(music.Title, music.Artist,music.Duration,music.PublicationYear);
            if(created != null)
            {

            
                return Ok(created);
            }
            else 
            {
                return BadRequest();
            }
            
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public ActionResult<Music> Put(int id, string title, string artist, int duration, DateTime publicationYear)
        {
             
            Music music = _musicRepo.Update(id, title, artist, duration, publicationYear);
            if (music != null)
            {


                return Ok(music);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public ActionResult<Music> Delete(int id)
        {
             
            Music music = _musicRepo.Delete(id);
            if (music != null)
            {


                return Ok(music);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
