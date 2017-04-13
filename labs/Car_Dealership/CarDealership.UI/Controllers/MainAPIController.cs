using CarDealership.DAL.Factories;
using CarDealership.Models.Queries;
using System;
using System.Web.Http;

namespace CarDealership.UI.Controllers
{
    public class MainAPIController : ApiController
    {
        [Route("api/vehicles/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(int? condition, decimal? minPrice, decimal? maxPrice,
            int? minYear, int? maxYear, bool isAspNetUser, string searchTerm)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    Condition    = condition,
                    MinPrice     = minPrice,
                    MaxPrice     = maxPrice,
                    MinYear      = minYear,
                    MaxYear      = maxYear,
                    IsAspNetUser = isAspNetUser,
                    SearchTerm   = searchTerm
                };

                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/makes")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Makes()
        {
            var repo = MakeRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/models")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Models()
        {
            var repo = ModelRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/models/makes/{makeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ModelsByMake(int makeId)
        {
            var repo = ModelRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetByMakeId(makeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/interiorcolors")]
        [AcceptVerbs("GET")]
        public IHttpActionResult InteriorColors()
        {
            var repo = ColorRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAllInterior();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/exteriorcolors")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ExteriorColors()
        {
            var repo = ColorRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAllExterior();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/specials")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Specials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/special/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Special(int id)
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            try
            {
                repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
