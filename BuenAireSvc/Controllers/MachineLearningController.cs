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
    public class MachineLearningController : ControllerBase
    {
        [HttpGet("{zoom}/{x}/{y}")]
        public ActionResult<FileStream> Get(uint zoom, int x, int y)
        {
            string physicalPath = $"Data/MLMOutput/aqlatestL{zoom}T{x.ToString("D2")}{y.ToString("D2")}.png";
            FileStream stream = System.IO.File.OpenRead(physicalPath);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return System.IO.File.OpenRead(physicalPath);
        }
    }
}
