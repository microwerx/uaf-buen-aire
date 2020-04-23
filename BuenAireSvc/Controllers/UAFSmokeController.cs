using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;

namespace BuenAireSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UAFSmokeController : ControllerBase
    {
        [HttpGet("{zoom}/{x}/{y}")]
        public ActionResult<FileStream> Get(uint zoom, int x, int y)
        {
            string physicalPath = $"Data/UAFSmoke/aqlatestL{zoom}T{x.ToString("D2")}{y.ToString("D2")}.png";
            return System.IO.File.OpenRead(physicalPath);
        }
    }
}
