using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//ASP_NET
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore;

namespace JAN0837_BP.WebApp
{
    [ApiController]
    [Route("api/test")]
    public class Controller : Microsoft.AspNetCore.Mvc.ControllerBase
    {

        [HttpGet("GetTest")]
        public IActionResult GetData()
        {
            return Ok("Test"); // show text
        }
    }
}
