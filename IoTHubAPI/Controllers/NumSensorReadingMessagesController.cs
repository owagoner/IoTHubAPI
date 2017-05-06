using IoTHubAPI.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IoTHubAPI.Controllers
{
    [Route("api/NumSensorReadingMessages")]
    public class NumSensorReadingMessagesController : ApiController
    {
        private string addSensorQueue = "addsensorqueue";
        private string sensorReadingQueue = "sensorreadingqueue";

        [HttpGet]
        public long Get()
        {
            ServiceBus sb = new ServiceBus();
            return sb.getNumberOfMessages(sensorReadingQueue);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 120942177)
            {
                ServiceBus sb = new ServiceBus();
                return await sb.deleteQueueMessagesAsync(sensorReadingQueue);
            }
            else {
                return false;
            }
            
        }
    }
}
