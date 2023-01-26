﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using AzureIOT.Model;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Azure.Devices.Shared;

namespace AzureIOT.Repositories
{
    public class SendTelemetryRepository
    {
        private static string connectionString = "HostName=iothub-sn230126.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=1FsmLQvj0w23xQ8wBzDqxezs7d2357ME1M3FZoorKgw=";
       // public static RegistryManager registryManager;
       // public static DeviceClient client = null;
        public static string myDeviceConnection= "HostName=iothub-sn230126.azure-devices.net;DeviceId=sensor-thl-01;SharedAccessKey=9wZNgDVx2/uEFmVmdeEyRUQDdav8j+p8XVNWVGmA2MY=";
        public static async Task SendMessage(string deviceName)
        {
            try
            {
                RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                var device = await registryManager.GetTwinAsync(deviceName);
                Properties properties = new Properties();
                TwinCollection Prop;
                Prop = device.Properties.Reported;
                DeviceClient client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while(true)
                {
                    var telemetry = new
                    {
                        temperature = Prop["temperature"],
                        humidity = Prop["humidity"]
                    };
                    var telemetryString = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, telemetryString);
                    await Task.Delay(1000);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
