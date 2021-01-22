using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer1;
using DataLayer1.QueryEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CassandraDataLayer.QueryEntities;


namespace ITMeetUps_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PredavaciPoPrezentacijamaController : ControllerBase
    {
        [HttpGet]
        [Route("PredavaciPoPrezentacijama")]
        public ActionResult PredavaciPoPrezentacijama()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiPredavacePoPrezentacijama());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("PredavaciPoPrezentaciji/{prezentacija}")]
        public ActionResult PredavaciPoPrezentaciji([FromRoute(Name = "prezentacija")] string prezentacija)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiPredavacePoPrezentaciji(prezentacija));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("DodajPredavacaPoPrezentaciji")]
        public ActionResult DodajPredavacaPoPrezentaciji([FromBody] PredavaciPoPrezentacijama predavacPoPrez)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.DodajPredavacaPoPrezentaciji(predavacPoPrez));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajPredavacaPoPrezentaciji/{predavac}")]
        public ActionResult AzurirajPredavacaPoPrezentaciji([FromBody] PredavaciPoPrezentacijama predavac, [FromRoute(Name = "predavac")] string noviPredavac)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.AzurirajPredavacaPoPrezentaciji(predavac, noviPredavac));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        [HttpDelete]
        [Route("ObrisiPredavacaPoPrezentaciji")]
        public ActionResult ObisiPredavacaPoPrezentaciji([FromBody] PredavaciPoPrezentacijama predavac)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiPredavacaPoPrezentaciji(predavac));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }



    }


}

