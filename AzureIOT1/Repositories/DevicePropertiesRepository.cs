using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using AzureIOT.Model;

namespace AzureIOT.Repositories
{
    //This is for Device
    public class DevicePropertiesRepository
    {
        private static string connectionString = "HostName=iothub-sn230201.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=FK7LjWHopijZ4ulwAX37SPcM5GffwZEDCLklP4hYqT4=";
        public static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        //public static DeviceClient client;
        private static string myDeviceConnection = "HostName=iothub-sn230201.azure-devices.net;DeviceId=sensor;SharedAccessKey=XBxBtrujFo+ZOizqnCCEYE3B+edC22OkpP4Phg1qCoU=";

        public static async Task UpdateReportedPropertiesAsync(string deviceId, Properties properties)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                DeviceClient client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties, connectivity;
                reportedProperties = new TwinCollection();
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                reportedProperties["connectivity"] = connectivity;
                reportedProperties["temperature"] = properties.temperature;
                reportedProperties["humidity"] = properties.humidity;

                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;
            }
        }

        public static async Task UpdateDesiredPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                DeviceClient client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection desiredProperties, telemetryConfig;
                desiredProperties = new TwinCollection();
                telemetryConfig = new TwinCollection();
                var device = await registryManager.GetTwinAsync(deviceId);
                telemetryConfig["temperature"] = 36;
                desiredProperties["telemetryConfig"] = telemetryConfig;
                device.Properties.Desired["telemetryConfig"] = telemetryConfig;

                await registryManager.UpdateTwinAsync(device.DeviceId, device, device.ETag);
                return;
            }
        }

        public static async Task<Twin> GetPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            var device = await registryManager.GetTwinAsync(deviceId);
            return device;
        }

        public static async Task UpdateTagPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceId);
                var patch =
                @"{
                    tags:{
                location:
                    {
                        region: 'India East'
                    }
                }
                }";

                DeviceClient client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);
                return;
            }
        }
    }
}
