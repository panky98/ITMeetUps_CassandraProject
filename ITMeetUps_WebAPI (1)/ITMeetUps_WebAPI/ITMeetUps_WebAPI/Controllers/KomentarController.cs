using CassandraDataLayer.QueryEntities;
using DataLayer1.QueryEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeetUps_WebAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class KomentarController : ControllerBase
    {
        [HttpGet]
        [Route("KomentariPoUserima")]
        public ActionResult VratiSveKomentarePoUserima()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiSveKomentarePoUserima());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("KomentariPoPrezentacijama")]
        public ActionResult VratiSveKomentarePoPrezentacijama()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiSveKomentarePoPrezentacijama());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("KomentarUsername/{username}")]
        public ActionResult VratiKomentareUsera([FromRoute(Name = "username")] string username)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiKomentareUsera(username));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("KomentarPrezentacija/{nazivPrezentacije}")]
        public ActionResult VratiKomentarePrezentacije([FromRoute(Name = "nazivPrezentacije")] string nazivPrezentacije)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiKomentarePrezentacije(nazivPrezentacije));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("DodajKomentar")]
        public ActionResult DodajKomentar([FromBody] Komentar komentar)
        {
            try
            {
                DataLayer1.DataProvider.DodajKomentarUseru(komentar);

                DataLayer1.DataProvider.DodajKomentarPrezentaciji(komentar);



                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiKomentar")]
        public ActionResult ObrisiKomentar([FromBody] Komentar komentar)
        {
            try
            {
                DataLayer1.DataProvider.ObrisiKomentarUsera(komentar);

              

                DataLayer1.DataProvider.ObrisiKomentarPrezentacije(komentar);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajKomentar")]
        public ActionResult AzurirajKomentar([FromBody] Komentar komentar)
        {
            try
            {
                DataLayer1.DataProvider.AzurirajKomentarPoUseru(komentar);

               

                DataLayer1.DataProvider.AzurirajKomentarPoPrezentacijama(komentar);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
