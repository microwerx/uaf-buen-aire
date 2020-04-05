using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuenAireSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurpleAirController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return System.IO.File.ReadAllText(@".\Data\alaska_sensors.json");
        }

        [HttpGet("{what}/{latSw}/{lonSw}/{latNe}/{lonNe}")]
        public ActionResult<string> Get(string what, double latSw, double lonSw, double latNe, double lonNe)
        {
            string file = System.IO.File.ReadAllText(@".\Data\alaska_sensors.json");
            JArray array = JArray.Parse(file);

            string result = "";
            if (what == "simple")
            {
                var simple = from sensor in array
                             where Convert.ToDouble(sensor["Lat"]) >= latSw && Convert.ToDouble(sensor["Lon"]) >= lonSw
                                    && Convert.ToDouble(sensor["Lat"]) <= latNe && Convert.ToDouble(sensor["Lon"]) <= lonNe
                                    && sensor.Value<string>("ParentID") == null
                             select new { Label = sensor["Label"], Lat = sensor["Lat"], Lon = sensor["Lon"], PM2_5 = sensor["PM2_5Value"] };

                result = JsonConvert.SerializeObject(simple);
            }
            else if (what == "all")
            {
                var all = from sensor in array
                             where Convert.ToDouble(sensor["Lat"]) >= latSw && Convert.ToDouble(sensor["Lon"]) >= lonSw
                                    && Convert.ToDouble(sensor["Lat"]) <= latNe && Convert.ToDouble(sensor["Lon"]) <= lonNe
                             select sensor;

                result = JsonConvert.SerializeObject(all);
            }
            

            return result;
        }
    }
}
