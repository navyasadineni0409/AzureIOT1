using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using AzureIOT.Repositories;



namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        [HttpPost("SendTelemetryMessage")]
        public async Task<string> SendMessage(string deviceName)
        {
            await SendTelemetryRepository.SendMessage(deviceName);
            return null;
        }
    }
}
