using CassandraDataLayer.QueryEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeetUps_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrezentacijaController : ControllerBase
    {
        [HttpGet]
        [Route("Prezentacije")]
        public ActionResult VratiSvePrezentacije()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiSvePrezentacije());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("Prezentacija/{naziv_prezentacije}")]
        public ActionResult VratiPrezentaciju([FromRoute(Name = "naziv_prezentacije")] string nazivPrezentacije)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiPrezentaciju(nazivPrezentacije));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("DodajPrezentaciju")]
        public ActionResult DodajPrezentaciju([FromBody] Prezentacija prezentacija)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.DodajPrezentaciju(prezentacija));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiPrezentaciju/{naziv_prezentacije}")]
        public ActionResult ObrisiPrezentaciju([FromRoute(Name = "naziv_prezentacije")] string nazivPrezentacije)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiPrezentaciju(nazivPrezentacije));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("UpdateInteresovanjaZaPrezentaciju/{naziv_prezentacije}/{interesovanje}")]
        public ActionResult UpdateInteresovanjaZaPrezentaciju([FromRoute(Name = "naziv_prezentacije")] string nazivPrezentacije, 
            [FromRoute(Name = "interesovanje")] string interesovanje)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.UpdateInteresovanjaZaPrezentaciju(nazivPrezentacije, interesovanje));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("UpdatePrezentacija/{naziv_prezentacije}/{predavac}")]
        public ActionResult UpdatePrezentaciju([FromRoute(Name = "naziv_prezentacije")] string nazivPrezentacije,
            [FromRoute(Name = "predavac")] string predavac)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.UpdatePrezentaciju(nazivPrezentacije, predavac));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
