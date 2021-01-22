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
    public class PrijavaPoPrezentacijamaController : ControllerBase
    {
        [HttpPost]
        [Route("PrijavaPoPrezentacijama")]
        public ActionResult DodajPrijavuPoPrezentaciji([FromBody] PrijavaPoPrezentaciji objekat)
        {
            if (DataProvider.DodajPrijavuPoPrezentacijama(objekat.username, objekat.naziv_prezentacije,true))
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
        [HttpGet]
        [Route("PrijavaPoPrezentacijama")]
        public ActionResult VratiSvePrijavePoPrezentacijama()
        {
            IList<PrijavaPoPrezentaciji> returnList = DataProvider.VratiSvePrijavePoPrezentacijama();
            if (returnList == null)
            {
                return Ok(new List<PrijavaPoPrezentaciji>());
            }

            return Ok(returnList);
        }
        [HttpGet]
        [Route("PrijavaPoPrezentacijama/{nazivPrezentacije}")]
        public ActionResult VratiSvePrijavePoPrezentacijamaPoPrezentaciji([FromRoute(Name = "nazivPrezentacije")] string nazivPrezentacije)
        {
            IList<PrijavaPoPrezentaciji> returnList = DataProvider.VratiSvePrijavePoPrezentacijamaPoPrezentaciji(nazivPrezentacije);
            if (returnList == null)
            {
                return Ok(new List<PrijavaPoPrezentaciji>());
            }

            return Ok(returnList);
        }
        [HttpDelete]
        [Route("PrijavaPoPrezentacijama/{nazivPrezentacije}")]
        public ActionResult ObrisiSvePrijavePoUserimaPoUsername([FromRoute(Name = "nazivPrezentacije")] string nazivPrezentacije)
        {
            if (DataProvider.ObrisiSvePrijavePoPrezentacijamaPoPrezentaciji(nazivPrezentacije))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
