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
        private static string connectionString = "HostName=iothub-sn230201.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=FK7LjWHopijZ4ulwAX37SPcM5GffwZEDCLklP4hYqT4=";
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
