using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace AzureIOT.Repositories
{
    public class DeviceRepository
    {
       // public static RegistryManager registryManager;
        private static string connectionString = "HostName=iothub-rgsn230120.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=aP6l1V0x1TjpjzRCvR0lBw2Z8FVnnxmikZxQB2aqAkg=";
        public static async Task AddDeviceAsync(string deviceName)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("deviceNamePlease");
            }
            Device device;
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            return;
        }
        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            return device;

        }
        public static async Task DeleteDeviceAync(string deviceId)
        {
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceNamePlease");
            }
            Device device = new Device(deviceId);
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Updated Sucessfully";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;
        }
    }
}
