﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BuenAireSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineLearningController : ControllerBase
    {
        [HttpGet("{zoom}/{x}/{y}")]
        public ActionResult<FileStream> Get(uint zoom, int x, int y)
        {
            return System.IO.File.OpenRead($"Data/MLMOutput/aqlatestL{zoom}T{x.ToString("D2")}{y.ToString("D2")}.png");
        }
    }
}
