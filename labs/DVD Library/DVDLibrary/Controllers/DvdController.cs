using DVDLibrary.Data;
using DVDLibrary.Data.Interfaces;
using DVDLibrary.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DVDLibrary.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        private IDvdRepository repo = RepositoryFactory.GetRepository();

        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllDvds()
        {
            var dvds = repo.GetAllDvds();
            return Ok(dvds);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdById(int id)
        {
            var dvd = repo.GetDvdById(id);

            if (dvd != null)
            {
                return Ok(dvd);
            }
            return NotFound();
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByTitle(string title)
        {
            var dvds = repo.GetDvdsByTitle(title);

            if (dvds != null)
            {
                return Ok(dvds);
            }
            return NotFound();
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByReleaseYear(int releaseYear)
        {
            var dvds = repo.GetDvdsByReleaseYear(releaseYear);

            if (dvds != null)
            {
                return Ok(dvds);
            }
            return NotFound();
        }

        [Route("dvds/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByDirector(string directorName)
        {
            var dvds = repo.GetDvdsByDirector(directorName);

            if (dvds != null)
            {
                return Ok(dvds);
            }
            return NotFound();
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByRating(string rating)
        {
            var dvds = repo.GetDvdsByRating(rating);

            if (dvds != null)
            {
                return Ok(dvds);
            }
            return NotFound();
        }

        [Route("dvd/")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddDvd(AddDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dvd = new Dvd()
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Director = request.Director,
                Rating = request.Rating,
                Notes = request.Notes
            };

            repo.AddDvd(dvd);
            return Created($"dvd/{dvd.Id}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(UpdateDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dvd = repo.GetDvdById(request.Id);

            if (dvd == null)
            {
                return NotFound();
            }

            dvd.Title = request.Title;
            dvd.ReleaseYear = request.ReleaseYear;
            dvd.Director = request.Director;
            dvd.Rating = request.Rating;
            dvd.Notes = request.Notes;

            repo.UpdateDvd(dvd);
            return Ok(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int id)
        {
            var dvd = repo.GetDvdById(id);

            if (dvd == null)
            {
                return NotFound();
            }

            repo.DeleteDvd(id);
            return Ok();
        }
    }
}