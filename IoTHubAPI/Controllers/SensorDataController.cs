using IoTHubAPI.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IoTHubAPI.Controllers
{
    [Route("api/SensorData")]
    public class SensorDataController : ApiController
    {
        private string CollectionId = "SensorData";

        [HttpGet]
        public IEnumerable<string> Get()
        {
            DocumentDb db = new DocumentDb();
            var rs = db.GetSensorMessages();
            yield return Newtonsoft.Json.JsonConvert.SerializeObject(rs);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            if (id == 214213232)
            {
                DocumentDb db = new DocumentDb();
                return await db.ClearDocumentCollectionAsync(CollectionId);
            }
            else {
                return false;
            }
            
        }

    }
}
