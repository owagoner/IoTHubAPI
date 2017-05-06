using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTHubAPI.Models
{
    public class SensorReadingMessage
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string sensorHash { get; set; }
        public string data { get; set; }
        public DateTime date { get; set; }
        public string signature { get; set; }
    }
}