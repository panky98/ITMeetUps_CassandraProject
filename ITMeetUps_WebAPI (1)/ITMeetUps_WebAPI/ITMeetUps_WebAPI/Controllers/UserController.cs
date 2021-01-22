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
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("User")]
        public ActionResult DodajPrijavuPoUserima([FromBody] User objekat)
        {
            if (DataProvider.DodajUsera(objekat))
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet]
        [Route("User")]
        public ActionResult VratiSveUsere()
        {
            IList<User> returnList = DataProvider.VratiSveUsere();
            if (returnList == null)
            {
                return Ok(new List<PrijavaPoUseru>());
            }

            return Ok(returnList);
        }
        [HttpGet]
        [Route("User/{username}")]
        public ActionResult VratiSveUsere([FromRoute(Name ="username")]string username)
        {
            User user = DataProvider.VratiUseraPoUsernameu(username);
            if (user == null)
            {
                return Ok(false);
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("User/{username}")]
        public ActionResult DodajInteresovanjaUseru([FromRoute(Name = "username")] string username,[FromBody]IList<string> newInteresovanja)
        {
            if(DataProvider.DodajInteresovanjaUseru(username,newInteresovanja))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("User/{username}")]
        public ActionResult ObrisiUsera([FromRoute(Name = "username")] string username)
        {
            if(DataProvider.ObrisiUsera(username))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
