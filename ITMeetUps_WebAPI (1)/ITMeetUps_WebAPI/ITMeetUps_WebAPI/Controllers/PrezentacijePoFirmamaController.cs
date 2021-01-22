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
    public class PrezentacijePoFirmamaController : ControllerBase
    {
        [HttpGet]
        [Route("SvePrezentacijePoFirmama")]
        public ActionResult SvePrezentacijePoFirmama()
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiSvePrezentacijePoFirmama());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("PrezentacijeZaFirmu/{pib}")]
        public ActionResult PrezentacijeZaFirmu([FromRoute(Name = "pib")] string pib)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.VratiPrezentacijeZaFirmu(pib));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("DodajPrezentacijuZaFirmu")]
        public ActionResult DodajPrezentacijuZaFirmu([FromBody] PrezentacijePoFirmama prezentacijaZaFirmu)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.DodajPrezentacijuZaFirmu(prezentacijaZaFirmu));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiPrezentacijuZaFirmu")]
        public ActionResult ObisiPrezentacijuZaFirmu([FromBody] PrezentacijePoFirmama prezentacijaZaFirmu)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiPrezentacijuZaFirmu(prezentacijaZaFirmu));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiSvePrezentacijeZaFirmu/{pib}")]
        public ActionResult ObrisiSvePrezentacijeZaFirmu([FromRoute(Name ="pib")] string pib)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiSvePrezentacijeZaFirmu(pib));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiSvePrezentacijeZaFirmuPoPrezentaciji/{naziv_prezentacije}")]
        public ActionResult ObrisiSvePrezentacijeZaFirmuPoPrezentaciji([FromRoute(Name = "naziv_prezentacije")] string naziv_prezentacije)
        {
            try
            {
                return new JsonResult(DataLayer1.DataProvider.ObrisiSvePrezentacijeZaFirmuPoPrezentaciji(naziv_prezentacije));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


    }
}
