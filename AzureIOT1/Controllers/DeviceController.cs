using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureIOT.Repositories;
using Microsoft.Azure.Devices;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpPost("AddIotDevice")]
        public async Task AddDevice(string deviceName)
        {
            await DeviceRepository.AddDeviceAsync(deviceName);
            return;
        }
        [HttpGet("GetIotDevice")]
        public async Task<Device> GetDeviceAsync(string deviceName)
        {
            Device device;
            device = await DeviceRepository.GetDeviceAsync(deviceName);
            return device;
        }
        [HttpDelete("DeleteIotDevice")]
        public async Task DeleteIotDevice(string deviceName)
        {
            await DeviceRepository.DeleteDeviceAync(deviceName);
            return;
        }
        [HttpPut("UpdateIotDevice")]
        public async Task<Device>UpdateDeviceProperties(string deviceName)
        {
            Device device;
            device = await DeviceRepository.UpdateDeviceAsync(deviceName);
            return device;
        }
    }
}
