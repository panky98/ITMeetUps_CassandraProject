using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer1;
using DataLayer1.QueryEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITMeetUps_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrijavaPoUserimaController : ControllerBase
    {
        [HttpPost]
        [Route("PrijavaPoUserima")]
        public ActionResult DodajPrijavuPoUserima([FromBody]PrijavaPoUseru objekat)
        {
            if (DataProvider.DodajPrijavuPoUserima(objekat.username, objekat.naziv_prezentacije,true))
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
        [HttpGet]
        [Route("PrijavaPoUserima")]
        public ActionResult VratiSvePrijavePoUserima()
        {
            IList<PrijavaPoUseru> returnList = DataProvider.VratiSvePrijavePoUserima();
            if(returnList==null)
            {
                return Ok(new List<PrijavaPoUseru>());
            }

            return Ok(returnList);
        }
        [HttpGet]
        [Route("PrijavaPoUserima/{username}")]
        public ActionResult VratiSvePrijavePoUserimaPoUsername([FromRoute(Name ="username")]string username)
        {
            IList<PrijavaPoUseru> returnList = DataProvider.VratiSvePrijavePoUserimaPoUsername(username);
            if (returnList == null)
            {
                return Ok(new List<PrijavaPoUseru>());
            }

            return Ok(returnList);
        }
        [HttpDelete]
        [Route("PrijavaPoUserima/{username}")]
        public ActionResult ObrisiSvePrijavePoUserimaPoUsername([FromRoute(Name = "username")] string username)
        {
            if (DataProvider.ObrisiSvePrijavePoUserimaPoUsername(username))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
