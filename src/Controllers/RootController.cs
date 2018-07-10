using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace whoami.Controllers
{
    [Route("")]
    [ApiController]
    public class RootController : ControllerBase
    {
        // GET
        [HttpGet]
        public ActionResult<string> Get()
        {
            var hostName = Dns.GetHostName();
            var nics = GetIpsFromNics();
            var result = new JsonResult(new { hostName, nics, this.HttpContext.Request.Headers }, new JsonSerializerSettings() { Formatting = Formatting.Indented });
            return result;
        }

        private IEnumerable<string> GetIpsFromNics()
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork || ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipAddrList.ToArray();
        }
    }
}
