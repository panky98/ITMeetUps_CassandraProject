using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CassandraDataLayer.QueryEntities;

namespace ITMeetUps_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InteresovanjePoUserimaController : ControllerBase
    {
        [HttpGet]
        [Route("InteresovanjaPoUserima")]
        public ActionResult InteresovanjaPoUserima()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiInteresovanjaPoUserima());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("InteresovanjaPoUseru/{username}")]
        public ActionResult InteresovanjaPoUseru([FromRoute(Name = "username")] string username)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiInteresovanjaPoUseru(username));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("DodajInteresovanjePoUseru")]
        public ActionResult DodajInteresovanjePoUseru([FromBody] InteresovanjePoUserima novoInt)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.DodajInteresovanjePoUseru(novoInt));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajInteresovanjePoUseru/{username}")]
        public ActionResult AzurirajInteresovanjePoUseru([FromBody] InteresovanjePoUserima interesovanje, [FromRoute(Name = "interesovanje")] string novoInteresovanje) //ne treba li obrnuto?
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.AzurirajInteresovanjePoUseru(interesovanje, novoInteresovanje));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        [HttpDelete]
        [Route("ObrisiInteresovanjePoUseru")]
        public ActionResult ObisiInteresovanjePoUseru([FromBody] InteresovanjePoUserima interes)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiInteresovanjaPoUseru(interes));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


    }
}
