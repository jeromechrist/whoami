using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace whoami.Controllers
{
    [Route("")]
    [ApiController]
    public class RootController : ControllerBase
    {
        // GET
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var hostName = Dns.GetHostName();
            return new string[] { hostName };
        }
    }
}
