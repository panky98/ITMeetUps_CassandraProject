using CassandraDataLayer.QueryEntities;
using DataLayer1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeetUps_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FirmaController : ControllerBase
    {
        [HttpGet]
        [Route("Firme")]
        public ActionResult VratiSveFirme()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiSveFirme());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("Firma/{pib}")]
        public ActionResult VratiFirmuPIB([FromRoute(Name = "pib")] string pib)
        {
            try
            {
                Firma firma = DataProvider.VratiFirmuPIB(pib);
                if (firma.pib == null && firma.naziv==null && firma.adresa==null)
                {
                    return Ok(false);
                }
                return Ok(firma);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("DodajFirmu")]
        public ActionResult DodajFirmu([FromBody] Firma firma)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.DodajFirmu(firma));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiFirmu/{pib}")]
        public ActionResult ObrisiFirmu([FromRoute(Name = "pib")] string pib)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiFirmu(pib));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateFirma/{pib}/{adresa}")]
        public ActionResult UpdateFirmu([FromRoute(Name = "pib")] string pib, [FromRoute(Name = "adresa")] string adresa)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.UpdateFirmu(pib, adresa));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


    }
}
